namespace SwimResults.Models
{
    using System.ComponentModel.DataAnnotations;
    using Constants.Enums;
    using NP.Helpers.Extensions;
    using SwimResults.Tools;

    public class WorkoutIntervalLengthViewModel
    {
        public int Id { get; set; }

        public int LengthNo { get; set; }

        public int WorkoutIntervalId { get; set; }

        public double Duration { get; set; }

        [Display(Name = "Stroke type")]
        public StrokeType StrokeTypeId { get; set; }

        [Display(Name = "Stroke count")]
        public int StrokeCount { get; set; }

        public double Distance { get; set; }

        public string StrokeTypeName => StrokeTypeId.GetDisplayName(); //{ get }

        public string DurationFormatted { get => DisplayValuesFormatter.FormatDuration(Duration, true); }

        [Display(Name = "Pace")]
        public string PaceFormatted => Distance > 0 ? DisplayValuesFormatter.FormatDuration(Duration * 100 / Distance, true) : "-";
    }
}
