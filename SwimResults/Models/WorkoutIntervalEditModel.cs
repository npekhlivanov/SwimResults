namespace SwimResults.Models
{
    using DataAccess.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

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
