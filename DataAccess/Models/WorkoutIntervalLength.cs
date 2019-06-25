namespace DataAccess.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class WorkoutIntervalLength
    {
        public enum StrokeType
        {
            [Description("Free")]
            Freestyle = 0,
            [Description("Back")]
            Backstroke = 1,
            [Description("Breast")]
            Breaststroke = 2,
            [Description("Fly")]
            Butterfly = 3,
            [Description("Drill")]
            Drill = 4,
            [Description("Kick")]
            Kick = 5
        }

        public int Id { get; set; }

        public int WorkoutIntervalId { get; set; }

        public float Duration { get; set; }

        [Display(Name = "Stroke type")]
        [System.Xml.Serialization.XmlElement("strokeType")]
        public StrokeType StrokeTypeId { get; set; }

        [Display(Name = "Stroke count")]
        public int StrokeCount { get; set; }

        public float Distance { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public string StrokeTypeName => StrokeTypeId.ToString(); //{ get }

        public virtual WorkoutInterval WorkoutInterval { get; set; }
    }
}
