using System;

namespace DataAccess.Models
{
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class WorkoutLocation
    {
        [System.Xml.Serialization.XmlElement("lat")]
        public decimal Latitude { get; set; }

        public decimal Lon { get; set; }

        [System.Xml.Serialization.XmlElement("long")]
        public decimal Longitude { get; set; }
    }
}
