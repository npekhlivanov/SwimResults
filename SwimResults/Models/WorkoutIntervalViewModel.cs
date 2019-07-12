namespace SwimResults.Models
{
    using DataAccess.Models;
    using Microsoft.AspNetCore.Mvc;
    using SwimResults.Tools;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class WorkoutIntervalViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public int WorkoutId { get; set; }

        [Display(Name = "Time offset")]
        public float TimeOffset { get; set; }

        public float Duration { get => Lengths?.Sum(x => x.Duration) ?? 0; }

        public float DurationComputed { get; set; }

        public float Distance { get => Lengths?.Sum(x => x.Distance) ?? 0; }

        public IList<WorkoutIntervalLengthViewModel> Lengths { get; set; }

        public int? WorkoutIntervalTypeId { get; set; }

        [Display(Name = "Interval type")]
        public WorkoutIntervalType WorkoutIntervalType { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        public string DurationFormatted { get => DisplayValuesFormatter.FormatDuration(Duration, false); }

        [Display(Name = "Pace")]
        public string PaceFormatted { get => Distance > 0 ? DisplayValuesFormatter.FormatDuration(Duration * 100 / Distance, true) : "-"; }

        [Display(Name = "Start time")]
        public string StartTime { get => DisplayValuesFormatter.FormatDuration(TimeOffset, false); }

        [Display(Name = "End time")]
        public string EndTime { get => DisplayValuesFormatter.FormatDuration(TimeOffset + Duration, false); }
    }
}
