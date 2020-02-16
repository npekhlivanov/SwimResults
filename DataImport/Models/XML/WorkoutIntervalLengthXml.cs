namespace DataImport.Models.XML
{
    using System.Xml.Serialization;

    //[System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType("workoutIntervalLength", AnonymousType = true)]
    public class WorkoutIntervalLengthXml
    {
        [XmlElement("duration")]
        public float Duration { get; set; }

        [XmlElement("strokeType")]
        public int StrokeType { get; set; }

        [XmlElement("strokeCount")]
        public int StrokeCount { get; set; }

        [XmlElement("distance")]
        public float Distance { get; set; }
    }
}
