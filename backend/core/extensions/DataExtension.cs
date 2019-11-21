using System;

namespace core.lib.extensions
{
    public static class DateTimeExtension
    {
        public static object AplicarOffset(this DateTime data, int offset)
        {
            if (offset > 0 && data < DateTime.MaxValue)
            {
                return data.AddHours(offset);
            }

            if (offset < 0 && data > DateTime.MinValue)
            {
                return data.AddHours(offset);
            }

            return data;
        }

        public static object AplicarOffset(this DateTime? data, int offset)
        {
            if (data.HasValue && offset > 0 && data.Value < DateTime.MaxValue)
            {
                return data.Value.AddHours(offset);
            }

            if (data.HasValue && offset < 0 && data > DateTime.MinValue)
            {
                return data.Value.AddHours(offset);
            }

            return data;
        }
    }
}
