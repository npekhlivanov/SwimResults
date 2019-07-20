namespace DataAccess.Models
{
    using Contracts.Entities;
    using DataTemplates.Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class WorkoutInterval : Entity, IWorkoutInterval
    {
        public int WorkoutId { get; set; }

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

        public virtual Workout Workout { get; set; }
    }
}
