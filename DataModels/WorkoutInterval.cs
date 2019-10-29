namespace DataModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Contracts.Entities;
    using DataTemplates.Entities;
    using static Constants.Enums;

    public class WorkoutInterval : Entity, IWorkoutInterval
    {
        public int WorkoutId { get; set; }

        public int? IntervalNo { get; set; }

        public float TimeOffset { get; set; }

        public float Duration { get; set; }

        public float Distance { get; set; }

        //[NotMapped]
        //public float DurationComputed { get => Lengths?.Sum(x => x.Duration) ?? 0; }

        //[NotMapped]
        //public float DistanceComputed { get => Lengths?.Sum(x => x.Distance) ?? 0; }

        public IList<WorkoutIntervalLength> Lengths { get; set; }

        public int? WorkoutIntervalTypeId { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        public float StrokeCount { get; set; }

        public WorkoutIntervalType WorkoutIntervalType { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)] // this is set in OnModelCreating() so it is redundant here
        public StrokeType? StrokeTypeId { get; private set; }
        public float? Pace { get; set; }

        public float? Swolf { get; set; }

        public virtual Workout Workout { get; set; }
    }
}
