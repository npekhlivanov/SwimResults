namespace SwimResults.Tools
{
    using System;
    using DataModels;

    public static class WourkoutExtensions
    {
        public static string GetDurationFormatted(this Workout workout)
        {
            if (workout == null)
            {
                throw new ArgumentNullException(nameof(workout));
            }

            return DisplayValuesFormatter.FormatDuration(workout.Duration, false);
        }

        public static string GetPaceFormatted(this Workout workout)
        {
            if (workout == null)
            {
                throw new ArgumentNullException(nameof(workout));
            }

            return DisplayValuesFormatter.FormatDuration(workout.Pace, false);
        }
    }
}
