namespace DataAccess.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class WorkoutInterval
    {
        public int Id { get; set; }

        public int WorkoutId { get; set; }

        [Display(Name = "Time offset")]
        public float TimeOffset { get; set; }

        [System.Xml.Serialization.XmlElement("length")]
        public ICollection<WorkoutIntervalLength> Lengths { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public int? WorkoutIntervalTypeId { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        [MaxLength(500)]
        public string Notes { get; set; }

        public WorkoutIntervalType WorkoutIntervalType { get; set; }

        public virtual Workout Workout { get; set; }
    }
}
