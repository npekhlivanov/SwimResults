namespace DataAccess.Models
{
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class WorkoutInterval
    {
        public int Id { get; set; }

        public int WorkoutId { get; set; }

        public float TimeOffset { get; set; }

        [System.Xml.Serialization.XmlElement("length")]
        public WorkoutIntervalLength[] Length { get; set; }
    }
}
