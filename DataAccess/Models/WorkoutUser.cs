using System;

namespace DataAccess.Models
{
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class WorkoutUser
    {
        public string Id { get; set; }

        public string Password { get; set; }
    }
}
