namespace DataImport.Tools
{
    using System;
    using DataAccess.Models;

    public class Converters
    {
        public static DateTime UnixTimeToDateTime(long unixTime)
        {
            var time = DateTimeOffset.FromUnixTimeMilliseconds(unixTime);
            return time.LocalDateTime;
        }

        public static WorkoutIntervalLength.StrokeType IntToStrokeType(int value)
        {
            if (Enum.IsDefined(typeof(WorkoutIntervalLength.StrokeType), value))
            {
                return (WorkoutIntervalLength.StrokeType)value;
            }

            throw new ArgumentOutOfRangeException($"{value} is not valid for type {typeof(WorkoutIntervalLength.StrokeType).Name}");
        }
    }
}
