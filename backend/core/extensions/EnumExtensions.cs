using System;
using System.ComponentModel;

namespace core.extensions
{
    public static class EnumExtensions
    {
        public static string ToDescriptionString(this Enum @enum)
        {
            var attributes = (DescriptionAttribute[])
                @enum.GetType()
                     .GetField(@enum.ToString())
                     .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
