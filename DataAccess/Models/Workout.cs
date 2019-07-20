namespace DataAccess.Models
{
    using DataTemplates.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts.Entities;

    public class Workout : Entity, IWorkout
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public float Distance { get; set; }

        public float Duration { get; set; }

        public float Pace { get; set; }

        [MaxLength(100)]
        public string Place { get; set; }

        [DataType(DataType.Date)] // Display the value without the time
        public DateTime Date { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Start { get; set; }

        [MaxLength(1000)]
        public string Note { get; set; }

        public IList<WorkoutInterval> Intervals { get; set; }

        public float ActiveTime { get; set; }

        public float CourseLength { get; set; }
    }
}
