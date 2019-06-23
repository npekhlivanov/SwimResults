namespace DataAccess.Models
{
    using System;

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class WorkoutDevice
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public string Deviceid { get; set; }
    }
}
