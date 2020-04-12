using Constants.Enums;

namespace SwimResults.Models.Api
{
    public class IntervalLengthDisplayData
    {
        public int LengthNo { get; set; }

        public string DurationFormatted { get; set; }

        public string DistanceFormatted { get; set; }

        public int StrokeCount { get; set; }

        public StrokeType StrokeTypeId { get; set; }

        public string PaceFormatted { get; set; }

        public string StrokeTypeName { get; set; }
    }
}
