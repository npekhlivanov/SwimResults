namespace DataModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Contracts.Entities;
    using DataTemplates.Entities;
    using static Constants.Enums;

    public class WorkoutInterval : Entity, IWorkoutInterval
    {
        public WorkoutInterval()
        {
            this.Lengths = new List<WorkoutIntervalLength>();
        }

        public int WorkoutId { get; set; }

        public int? IntervalNo { get; set; }

        public double TimeOffset { get; set; }

        public double Duration { get; set; }

        public double Distance { get; set; }

        //[NotMapped]
        //public float DurationComputed { get => Lengths?.Sum(x => x.Duration) ?? 0; }

        //[NotMapped]
        //public float DistanceComputed { get => Lengths?.Sum(x => x.Distance) ?? 0; }

        public ICollection<WorkoutIntervalLength> Lengths { get; }

        public int? WorkoutIntervalTypeId { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        public double StrokeCount { get; set; }

        public WorkoutIntervalType WorkoutIntervalType { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)] // this is set in OnModelCreating() so it is redundant here
        public StrokeType? StrokeTypeId { get; private set; }
        public double? Pace { get; set; }

        public double? Swolf { get; set; }

        public virtual Workout Workout { get; set; }
    }
}
