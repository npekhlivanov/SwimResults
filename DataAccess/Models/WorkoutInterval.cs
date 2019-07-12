﻿namespace DataAccess.Models
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

        [Display(Name = "Time offset")]
        public float TimeOffset { get; set; }

        [NotMapped]
        public float Duration { get => Lengths?.Sum(x => x.Duration) ?? 0; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public float DurationComputed { get; set; }

        [NotMapped]
        public float Distance { get => Lengths?.Sum(x => x.Distance) ?? 0; }

        public IList<WorkoutIntervalLength> Lengths { get; set; }

        public int? WorkoutIntervalTypeId { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        public WorkoutIntervalType WorkoutIntervalType { get; set; }

        public virtual Workout Workout { get; set; }
    }
}
