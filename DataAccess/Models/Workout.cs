namespace DataAccess.Models
{
    using System;

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class Workout
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Distance { get; set; }

        public float Duration { get; set; }

        public float Pace { get; set; }

        public string Place { get; set; }

        public WorkoutDevice Device { get; set; }

        public WorkoutUser User { get; set; }

        public WorkoutLocation Location { get; set; }

        [System.Xml.Serialization.XmlElement(DataType = "date")]
        public DateTime Start { get; set; }

        public WorkoutCourse Course { get; set; }

        public string Note { get; set; }

        [System.Xml.Serialization.XmlElement("interval")]
        public WorkoutInterval[] Intervals { get; set; }
    }
}
