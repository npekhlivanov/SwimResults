namespace SwimResults.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DataModels;
    using Microsoft.AspNetCore.Mvc;
    using SwimResults.Tools;
    using static Constants.Enums;

    public class WorkoutIntervalViewModel
    {
        public WorkoutIntervalViewModel()
        {
            this.Lengths = new List<WorkoutIntervalLengthViewModel>();
        }

        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public int WorkoutId { get; set; }

        [Display(Name = "Interval №")]
        public int IntervalNo { get; set; }

        [Display(Name = "Time offset")]
        public float TimeOffset { get; set; }

        //public float DurationComputed { get => Lengths?.Sum(x => x.Duration) ?? 0; }

        public float Duration { get; set; }

        public float Distance { get; set; }

        public List<WorkoutIntervalLengthViewModel> Lengths { get; }

        public int? WorkoutIntervalTypeId { get; set; }

        [Display(Name = "Interval type")]
        public WorkoutIntervalType WorkoutIntervalType { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Display(Name = "Stroke count")]
        public float StrokeCount { get; set; }

        [Display(Name = "Stroke type")]
        public StrokeType StrokeTypeId { get; set; }

        public float Pace { get; set; }

        [Display(Name = "SWOLF")]
        public float Swolf { get; set; }

        public string DistanceFormatted => Distance > 0 ? Distance.ToStringInvariant() + " m" : "-";

        public string StrokeCountFormatted => StrokeCount > 0 ? StrokeCount.ToStringInvariant("0.#") : "-";

        public string DurationFormatted { get => DisplayValuesFormatter.FormatDuration(Duration, false); }

        public string DurationFormattedWithMs { get => DisplayValuesFormatter.FormatDuration(Duration, true); }

        [Display(Name = "Pace")]
        public string PaceFormatted { get => Distance > 0 ? DisplayValuesFormatter.FormatDuration(Pace, true) : "-"; }

        public string SwolfFormatted => StrokeTypeId != StrokeType.Drill && StrokeCount > 0 ? Swolf.ToStringInvariant("0.#") : "-";

        [Display(Name = "Start time")]
        public string StartTime { get => DisplayValuesFormatter.FormatDuration(TimeOffset, false); }

        [Display(Name = "End time")]
        public string EndTime { get => DisplayValuesFormatter.FormatDuration(TimeOffset + Duration, false); }

        [Display(Name = "Workout name")]
        public string WorkoutName { get; set; }

        [Display(Name = "Workout date")]
        [DataType(DataType.Date)]
        public DateTime WorkoutDate { get; set; }

        public string StrokeTypeName => StrokeTypeId.GetDisplayName();
    }
}
