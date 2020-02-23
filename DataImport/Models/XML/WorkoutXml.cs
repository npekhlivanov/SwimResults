namespace DataImport.Models.XML
{
    using System;
    using System.Xml.Serialization;

    //[Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    //[System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    [XmlRoot("workout")]
    public class WorkoutXml
    {
        [XmlElement("device")]
        public WorkoutDeviceXml Device { get; set; }

        [XmlElement("user")]
        public WorkoutUserXml User { get; set; }

        [XmlElement("location")]
        public WorkoutLocationXml Location { get; set; }

        [XmlElement("start", DataType = "date")]
        //[XmlElement("start")]
        //[XmlChoiceIdentifier()]
        public DateTime Start { get; set; }

        [XmlElement("duration")]
        public double Duration { get; set; }

        [XmlElement("course")]
        public WorkoutCourseXml Course { get; set; }

        [XmlElement("note", DataType = "string")]
        public string Note { get; set; }

        [XmlElement("interval")]
        public WorkoutIntervalXml[] Intervals { get; set; }
    }
}
