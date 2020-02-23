namespace DataImport.Models.XML
{
    using System.Xml.Serialization;

    //[Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType("workoutCourse", AnonymousType = true)]
    public class WorkoutCourseXml
    {
        [XmlElement("courseLength")]
        public double CourseLength { get; set; }

        [XmlElement("courseUnit")]
        public string CourseUnit { get; set; }
    }
}
