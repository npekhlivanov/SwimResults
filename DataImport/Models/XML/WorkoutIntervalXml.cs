namespace DataImport.Models.XML
{
    using System.Xml.Serialization;

    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType("workoutInterval", AnonymousType = true)]
    public class WorkoutIntervalXml
    {
        [XmlElement("timeOffset")]
        public float TimeOffset { get; set; }

        [XmlElement("length")]
        public WorkoutIntervalLengthXml[] Lengths { get; set; }
    }
}
