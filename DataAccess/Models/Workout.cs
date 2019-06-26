namespace DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Workout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public float Distance { get; set; }

        public float Duration { get; set; }

        public float Pace { get; set; }

        [MaxLength(50)]
        public string Place { get; set; }

        public DateTime Date { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "datetime")]
        [Display(Name = "Start time")]
        public DateTime Start { get; set; }

        [Display(Name = "Notes")]
        [MaxLength(500)]
        public string Note { get; set; }

        public ICollection<WorkoutInterval> Intervals { get; set; }
    }
}
