namespace SwimResults.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;
    using SwimResults.Tools;

    public class WorkoutViewModel
    {
        public WorkoutViewModel()
        {
            this.Intervals = new List<WorkoutIntervalViewModel>();
        }

        [HiddenInput]
        public int Id { get; set; }

        public string Name { get; set; }

        public float Distance { get; set; }

        public float Duration { get; set; }

        public float Pace { get; set; }

        [MaxLength(100)]
        public string Place { get; set; }

        [DataType(DataType.Date)] // Display the value without the time
        public DateTime Date { get; set; }

        [Display(Name = "Start time")]
        public DateTime Start { get; set; }

        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        public List<WorkoutIntervalViewModel> Intervals { get; }

        public int CourseLength { get; set; }

        [Display(Name = "Active time")]
        public float ActiveTime { get; set; }

        [Display(Name = "Rest time")]
        public float RestTime => Duration - ActiveTime;

        public string DurationFormatted { get => DisplayValuesFormatter.FormatDuration(Duration, false); }

        public string PaceFormatted { get => DisplayValuesFormatter.FormatDuration(Pace, false); }

        public string ActiveTimeFormatted { get => DisplayValuesFormatter.FormatDuration(ActiveTime, false); }

        public string RestTimeFormatted { get => DisplayValuesFormatter.FormatDuration(RestTime, false); }
    }
}
