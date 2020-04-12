using System;
using System.Globalization;
using Constants.Enums;
using DataModels;
using NP.Helpers.Extensions;
using SwimResults.Models.Api;

namespace SwimResults.Tools
{
    public static class DisplayDataHelper
    {
        internal static IntervalDisplayData CreateIntervalDisplayData(WorkoutInterval interval)
        {
            var pace = interval.Distance == 0 ? 0 : interval.Duration * 100 / interval.Distance;
            var swolf = interval.Distance == 0 ? 0 : interval.Duration * 50 / interval.Distance + interval.StrokeCount;
            var result = new IntervalDisplayData
            {
                IntervalId = interval.Id,
                IntervalNo = interval.IntervalNo.GetValueOrDefault(),
                DistanceFormatted = interval.Distance > 0 ? interval.Distance.ToStringInvariant() + " m" : "-",
                DurationFormatted = DisplayValuesFormatter.FormatDuration(interval.Duration, true),
                StartTime = DisplayValuesFormatter.FormatDuration(interval.TimeOffset, false),
                StrokeCountFormatted = interval.StrokeCount > 0 ? interval.StrokeCount.ToStringInvariant("0.#") : "-",
                PaceFormatted = interval.Distance > 0 ? DisplayValuesFormatter.FormatDuration(pace, true) : "-",
                SwolfFormatted = interval.StrokeTypeId != StrokeType.Drill && interval.StrokeCount > 0 ? swolf.ToStringInvariant("0.#") : "-",
                IntervalTypeId = interval.WorkoutIntervalTypeId.GetValueOrDefault(),
                IntervalTypeName = interval.WorkoutIntervalType?.Name
            };

            foreach (var length in interval.Lengths)
            {
                result.Lengths.Add(new IntervalLengthDisplayData
                {
                    LengthNo = length.LengthNo.GetValueOrDefault(),
                    DurationFormatted = DisplayValuesFormatter.FormatDuration(length.Duration, true),
                    DistanceFormatted = $"{length.Distance} m",
                    PaceFormatted = length.Distance > 0 ? DisplayValuesFormatter.FormatDuration(length.Duration * 100 / length.Distance, true) : "-",
                    StrokeCount = length.StrokeCount,
                    StrokeTypeId = length.StrokeTypeId,
                    StrokeTypeName = length.StrokeTypeId.GetDisplayName()
                });
            }

            return result;
        }

        internal static WorkoutInterval CreateInterval(IntervalDisplayData inputInterval)
        {
            var interval = new WorkoutInterval
            {
                Id = inputInterval.IntervalId,
                IntervalNo = inputInterval.IntervalNo,
                Duration = ParseDuration(inputInterval.DurationFormatted),
                Distance = ParseDistance(inputInterval.DistanceFormatted),
                TimeOffset = ParseDuration(inputInterval.StartTime),
                WorkoutIntervalTypeId = inputInterval.IntervalTypeId,
                StrokeCount = double.Parse(inputInterval.StrokeCountFormatted, NumberFormatInfo.InvariantInfo)
            };

            foreach (var length in inputInterval.Lengths)
            {
                var duration = ParseDuration(length.DurationFormatted);
                var distance = ParseDistance(length.DistanceFormatted);
                interval.Lengths.Add(new WorkoutIntervalLength
                {
                    WorkoutIntervalId = inputInterval.IntervalId,
                    LengthNo = length.LengthNo,
                    Distance = distance,
                    StrokeTypeId = length.StrokeTypeId,
                    StrokeCount = length.StrokeCount,
                    Duration = duration,
                    WorkoutInterval = interval
                });
            }

            return interval;
        }

        private static double ParseDuration(string value)
        {
            double duration = 0;
            var durationParts = value.Split(':');
            if (durationParts.Length > 1)
            {
                if (int.TryParse(durationParts[0], out int min) && double.TryParse(durationParts[1], out double sec))
                {
                    duration = min * 60 + sec;
                }
            }

            return duration;
        }

        private static double ParseDistance(string value)
        {
            var distanceStr = value.Replace(" m", string.Empty, StringComparison.OrdinalIgnoreCase);
            if (!double.TryParse(distanceStr, out double distance))
            {
                distance = 0;
            }

            return distance;
        }
    }
}
