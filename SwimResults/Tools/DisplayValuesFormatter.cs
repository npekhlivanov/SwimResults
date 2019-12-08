namespace SwimResults.Tools
{
    using System;

    public static class DisplayValuesFormatter
    {
        public static string FormatDuration(float duration, bool showHundredths)
        {
            var time = TimeSpan.FromSeconds(duration);
            if (duration >= 3600)
            {
                return time.ToString(@"h\:mm\:ss");
            }

            return showHundredths ? time.ToString(@"mm\:ss\.ff") : time.ToString(@"mm\:ss");
        }
    }
}
