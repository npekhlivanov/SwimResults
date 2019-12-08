namespace DataImport.Models.XML
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
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
