namespace SwimResults.Tools
{
    using DataModels;

    public static class WourkoutExtensions
    {
        public static string GetDurationFormatted(this Workout workout) => DisplayValuesFormatter.FormatDuration(workout.Duration, false);
        public static string GetPaceFormatted(this Workout workout) => DisplayValuesFormatter.FormatDuration(workout.Pace, false);
    }
}
