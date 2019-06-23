namespace DataAccess.Models
{
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class WorkoutIntervalLength
    {
        public int Id { get; set; }

        public int WorkoutIntervalId { get; set; }

        public float Duration { get; set; }

        public int StrokeType { get; set; }

        public int StrokeCount { get; set; }

        public float Distance { get; set; }
    }
}
