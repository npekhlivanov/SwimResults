namespace SwimResults.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;

    public class WorkoutIntervalEditModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public int WorkoutId { get; set; }

        //[HiddenInput(DisplayValue = true)]
        public int IntervalNo { get; set; }

        //[HiddenInput]
        //[Display(Name = "Time offset")]
        //public float TimeOffset { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Display(Name = "Interval type")]
        public int? WorkoutIntervalTypeId { get; set; }

        //public WorkoutIntervalType WorkoutIntervalType { get; set; }
    }
}
