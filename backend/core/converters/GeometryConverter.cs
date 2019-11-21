using NetTopologySuite;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace web.Converters
{
    public class GeometryConverter : JsonConverter
    {
        public static int SRID = 4674;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Geometry)
                || objectType.BaseType == typeof(Geometry)
                || objectType.BaseType == typeof(GeometryCollection);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return null;
                }

                if (reader.TokenType == JsonToken.String)
                {
                    return ParseGeometry(reader.Value.ToString());
                }

                var feature = serializer.Deserialize<Feature>(reader);
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: SRID);
                var geo = geometryFactory.CreateGeometry(feature?.Geometry);
                return geo;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                var feature = new Feature(value as Geometry, new AttributesTable());
                GeoJsonSerializer.Create(new GeometryFactory(new PrecisionModel(), SRID)).Serialize(writer, feature);
            }
        }

        private static Geometry ParseGeometry(string valor)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                valor = Regex.Replace(valor, @"\s", "").Replace(@"\", "").Replace("\"", "");
                if (IsPoint(valor, out Coordinate coordinate))
                {
                    var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: SRID);
                    var geo = (Geometry)geometryFactory.CreatePoint(coordinate);
                    return geo;
                }
                else if (IsPolygon(valor, out Coordinate[] coordinates))
                {
                    var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: SRID);
                    var geo = (Geometry)geometryFactory.CreatePolygon(coordinates);
                    return geo;
                }
            }

            return null;
        }

        private static bool IsPoint(string valor, out Coordinate coordinate)
        {
            if (valor.StartsWith("{") && valor.EndsWith("}"))
            {
                valor = valor.Replace("{", "").Replace("}", "");
                var coordenadas = valor.Split(',');
                if (coordenadas.Length == 2
                    && coordenadas[0].StartsWith("longitude:")
                    && coordenadas[1].StartsWith("latitude:"))
                {
                    var strLongitude = coordenadas[0].Replace("longitude:", "");
                    var strLatitude = coordenadas[1].Replace("latitude:", "");
                    if (NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == ",")
                    {
                        strLongitude = strLongitude.Replace('.', ',');
                        strLatitude = strLatitude.Replace('.', ',');
                    }
                    if (double.TryParse(strLongitude, out double longitude)
                        && double.TryParse(strLatitude, out double latitude))
                    {
                        coordinate = new Coordinate(longitude, latitude);
                        return true;
                    }
                }
                else if (coordenadas.Length == 2
                    && coordenadas[0].StartsWith("latitude:")
                    && coordenadas[1].StartsWith("longitude:"))
                {
                    var strLatitude = coordenadas[0].Replace("latitude:", "");
                    var strLongitude = coordenadas[1].Replace("longitude:", "");
                    if (NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == ",")
                    {
                        strLatitude = strLatitude.Replace('.', ',');
                        strLongitude = strLongitude.Replace('.', ',');
                    }
                    if (double.TryParse(strLatitude, out double latitude)
                        && double.TryParse(strLongitude, out double longitude))
                    {
                        coordinate = new Coordinate(longitude, latitude);
                        return true;
                    }
                }
            }

            coordinate = null;
            return false;
        }

        private static bool IsPolygon(string valor, out Coordinate[] coordinates)
        {
            if (valor.StartsWith("[") && valor.EndsWith("]"))
            {
                valor = valor.Replace("[", "").Replace("]", "");
                valor = valor.Replace("},", "};");
                var listaCoordinates = new List<Coordinate>();
                foreach (string point in valor.Split(';'))
                {
                    if (IsPoint(point, out Coordinate coordinate))
                    {
                        listaCoordinates.Add(coordinate);
                    }
                    else
                    {
                        coordinates = new Coordinate[0];
                        return false;
                    }
                }

                coordinates = listaCoordinates.ToArray();
                return true;
            }

            coordinates = new Coordinate[0];
            return false;
        }
    }
}
