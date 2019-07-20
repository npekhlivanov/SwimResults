namespace SwimResults.Models
{
    using Microsoft.AspNetCore.Mvc;
    using SwimResults.Tools;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class WorkoutViewModel 
    {
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

        public IList<WorkoutIntervalViewModel> Intervals { get; set; }

        public string DurationFormatted { get => DisplayValuesFormatter.FormatDuration(Duration, false); }

        public string PaceFormatted { get => DisplayValuesFormatter.FormatDuration(Pace, false); }

        public int CourseLength { get; set; }

        public float ActiveTime { get; set; }
    }
}
