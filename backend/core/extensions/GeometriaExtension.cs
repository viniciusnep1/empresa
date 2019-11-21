using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System.Linq;

namespace core.lib.extensions
{
    public static class GeometriaExtension
    {
        public static object ConvertGeometriaToMobile(this Geometry geometry)
        {
            if (geometry != null)
            {
                object geom;
                switch (geometry.OgcGeometryType)
                {
                    case OgcGeometryType.Point:
                        geom = ConvertCoordinate(geometry.Coordinate.X, geometry.Coordinate.Y);
                        break;
                    case OgcGeometryType.Polygon:
                    case OgcGeometryType.LineString:
                    case OgcGeometryType.MultiPolygon:
                    case OgcGeometryType.MultiPoint:
                        geom = geometry.Coordinates.Select(c => ConvertCoordinate(c.X, c.Y));
                        break;
                    default:
                        geom = null;
                        break;
                }

                return JsonConvert.SerializeObject(geom);
            }

            return null;
        }

        private static object ConvertCoordinate(double x, double y)
        {
            return new { latitude = y, longitude = x };
        }
    }
}
