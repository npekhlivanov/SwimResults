namespace SwimResults.Services
{
    using System.Linq;
    using Constants;
    using Constants.Enums;
    using DataModels;
    using NP.Helpers;

    public static class WorkoutIntervalModifier
    {
        public static int AutoFillIntervalTypes(Workout workout)
        {
            var orderedIntervals = Validators.ValidateNotNull(workout, nameof(workout)).Intervals
                .OrderBy(i => i.IntervalNo)
                .ToArray();
            int result = 0;
            for (int i = 0; i < orderedIntervals.Length; ++i)
            {
                var interval = orderedIntervals[i];
                if (interval.WorkoutIntervalTypeId.HasValue)
                {
                    continue;
                }

                var typeId = interval.StrokeTypeId switch
                {
                    StrokeType.Breaststroke =>
                        (interval.IntervalNo == 1 || interval.IntervalNo == orderedIntervals.Length - 1) &&
                        interval.Duration == 150 &&
                        interval.Distance == 100 ? WorkoutIntervalTypes.PreWarmUpId : 0,
                    StrokeType.Backstroke => WorkoutIntervalTypes.BackstrokeId,
                    StrokeType.Drill => interval.StrokeCount == 0 && interval.Distance >= 300 ? WorkoutIntervalTypes.DrillWithFinsId : 0,
                    StrokeType.Freestyle => GetFreestyleIntervalType(interval, orderedIntervals),
                    _ => 0
                };

                if (typeId > 0)
                {
                    interval.WorkoutIntervalTypeId = typeId;
                    ++result;
                }
            }

            return result;
        }

        private static int GetFreestyleIntervalType(WorkoutInterval interval, WorkoutInterval[] orderedIntervals)
        {
            var prevIntervalType = interval.IntervalNo.GetValueOrDefault() > 1 ? orderedIntervals[interval.IntervalNo.Value - 2].WorkoutIntervalTypeId.GetValueOrDefault() : 0;
            if (interval.Distance == 100)
            {
                if (interval.Duration > 120)
                {
                    if (interval.IntervalNo == 1 || prevIntervalType == WorkoutIntervalTypes.PreWarmUpId)
                    {
                        return WorkoutIntervalTypes.WarmUpId;
                    }
                }
                else
                {
                    // interval.Duration <= 120
                    if (interval.IntervalNo > 1 && prevIntervalType == WorkoutIntervalTypes.WarmUpId)
                    {
                        return WorkoutIntervalTypes.FirstQuickFreestyleId;
                    }

                    if (interval.IntervalNo > 2 && orderedIntervals.Any(i => i.WorkoutIntervalTypeId == WorkoutIntervalTypes.FirstQuickFreestyleId))
                    {
                        return WorkoutIntervalTypes.SecondQuickFreestyleId;
                    }

                    if (interval.IntervalNo >= orderedIntervals.Length - 1)
                    {
                        return orderedIntervals.Any(i => i.WorkoutIntervalTypeId == WorkoutIntervalTypes.FinalQuickFreestyleId)
                           ? WorkoutIntervalTypes.FinalQuickFreestyle2Id
                           : WorkoutIntervalTypes.FinalQuickFreestyleId;
                    }
                }
            }

            if (interval.Distance >= 400)
            {
                if (!orderedIntervals.Any(i => i.WorkoutIntervalTypeId == WorkoutIntervalTypes.FreestyleSeriesId))
                {
                    return WorkoutIntervalTypes.FreestyleSeriesId;
                }
            }

            return 0;
        }
    }
}
