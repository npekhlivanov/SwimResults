namespace DataAccess.Models
{
    using Contracts.Entities;
    using DataTemplates.Entities;
    using System.ComponentModel.DataAnnotations;
    using static Constants.Enums;

    public class WorkoutIntervalLength : Entity, IWorkoutIntervalLength
    {
        public int WorkoutIntervalId { get; set; }

        public float Duration { get; set; }

        [Display(Name = "Stroke type")]
        public StrokeType StrokeTypeId { get; set; }

        [Display(Name = "Stroke count")]
        public int StrokeCount { get; set; }

        public float Distance { get; set; }

        public string StrokeTypeName => StrokeTypeId.ToString(); //{ get }

        public virtual WorkoutInterval WorkoutInterval { get; set; }
    }
}
