namespace DataImport.Models.XML
{
    using System.Xml.Serialization;

    //[System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType("workoutDevice", AnonymousType = true)]
    public class WorkoutDeviceXml
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("deviceid")]
        public string Deviceid { get; set; }
    }
}
