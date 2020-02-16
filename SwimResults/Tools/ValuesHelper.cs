namespace SwimResults.Tools
{
    using System;
    using System.Globalization;

    public static class ValuesHelper
    {
        public static string ComposeWorkoutName(DateTime dateAndTime)
        {
            var morningOrAfternoon = dateAndTime.Hour < 12 ? "Morning" : "Afternoon";
            var result = 
                $"{dateAndTime.ToString("dddd", DateTimeFormatInfo.InvariantInfo)} {morningOrAfternoon} Swim {dateAndTime.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo)}";
            return result;
        }

        public static int GetPageIndex(int? pageNo)
        {
            var pageIndex = pageNo.HasValue ? pageNo.Value > 1 ? pageNo.Value - 1 : 0 : 0;
            return pageIndex;
        }
    }
}
