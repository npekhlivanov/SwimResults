namespace DataImport.Models.XML
{
    using System.Xml.Serialization;

    //[System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType("workoutLocation", AnonymousType = true)]
    public class WorkoutLocationXml
    {
        [XmlElement("lat")]
        public decimal Latitude { get; set; }

        [XmlElement("lon")]
        public decimal Lon { get; set; }

        [XmlElement("long")]
        public decimal Longitude { get; set; }
    }
}
