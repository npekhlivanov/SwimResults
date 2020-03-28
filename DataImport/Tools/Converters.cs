namespace DataImport.Tools
{
    using System;
    using Constants.Enums;

    internal static class Converters
    {
        public static DateTime UnixTimeToDateTime(long unixTime)
        {
            var time = DateTimeOffset.FromUnixTimeMilliseconds(unixTime);
            return time.LocalDateTime;
        }

        public static StrokeType IntToStrokeType(int value)
        {
            if (Enum.IsDefined(typeof(StrokeType), value))
            {
                return (StrokeType)value;
            }

            throw new ArgumentOutOfRangeException($"{value} is not valid for type {typeof(StrokeType).Name}");
        }
    }
}
