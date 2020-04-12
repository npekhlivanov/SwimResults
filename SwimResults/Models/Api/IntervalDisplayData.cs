using System.Collections.Generic;

namespace SwimResults.Models.Api
{
    public class IntervalDisplayData
    {
        public IntervalDisplayData()
        {
            this.Lengths = new List<IntervalLengthDisplayData>();
        }

        public int IntervalId { get; set; }

        public int IntervalNo { get; set; }

        public string DurationFormatted { get; set; }

        public string StartTime { get; set; }

        public string PaceFormatted { get; set; }

        public string DistanceFormatted { get; set; }

        public string StrokeCountFormatted { get; set; }

        public string SwolfFormatted { get; set; }

        public int IntervalTypeId { get; set; }

        public string IntervalTypeName { get; set; }

        public IList<IntervalLengthDisplayData> Lengths { get; }
    }
}
