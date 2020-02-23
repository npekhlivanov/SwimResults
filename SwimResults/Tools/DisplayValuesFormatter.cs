namespace SwimResults.Tools
{
    using System;
    using NP.Helpers.Extensions;

    public static class DisplayValuesFormatter
    {
        public static string FormatDuration(double duration, bool showHundredths)
        {
            var time = TimeSpan.FromSeconds(duration);
            if (duration >= 3600)
            {
                return time.ToStringInvariant(@"h\:mm\:ss");
            }

            if (!showHundredths)
            {
                return time.ToStringInvariant(@"mm\:ss");
            }

            var roundedMs = time.Milliseconds != 0 ? Math.Round((double)time.Milliseconds / 10, 0) : 0;
            return $"{time.Minutes:00}:{time.Seconds:00}.{roundedMs:00}";
            //return time.ToString(@"mm\:ss\.ff") : ;
        }
    }
}
