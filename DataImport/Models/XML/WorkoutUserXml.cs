namespace DataImport.Models.XML
{
    using System.Xml.Serialization;

    //[System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType("workoutUser", AnonymousType = true)]
    public class WorkoutUserXml
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("password")]
        public string Password { get; set; }
    }
}
