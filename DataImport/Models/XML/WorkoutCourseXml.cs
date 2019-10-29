namespace DataImport.Models.XML
{
    using System.Xml.Serialization;

    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType("workoutCourse", AnonymousType = true)]
    internal class WorkoutCourseXml
    {
        [XmlElement("courseLength")]
        public float CourseLength { get; set; }

        [XmlElement("courseUnit")]
        public string CourseUnit { get; set; }
    }
}
