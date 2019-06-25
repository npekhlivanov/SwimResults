namespace DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class Workout
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

        [NotMapped]
        public WorkoutDevice Device { get; set; }

        [NotMapped]
        public WorkoutUser User { get; set; }

        [NotMapped]
        public WorkoutLocation Location { get; set; }

        [System.Xml.Serialization.XmlElement(DataType = "date")]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime")]
        public DateTime Start { get; set; }

        [NotMapped]
        public WorkoutCourse Course { get; set; }

        [Display(Name = "Notes")]
        [MaxLength(500)]
        public string Note { get; set; }

        [System.Xml.Serialization.XmlElement("interval")]
        public ICollection<WorkoutInterval> Intervals { get; set; }
    }
}
