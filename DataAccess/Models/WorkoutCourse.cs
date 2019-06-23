namespace DataAccess.Models
{
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class WorkoutCourse
    {

        public float CourseLength { get; set; }

        public string CourseUnit { get; set; }
    }
}
