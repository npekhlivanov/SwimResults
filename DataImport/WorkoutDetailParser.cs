namespace DataImport
{
    using DataAccess.Models;
    using DataImport.Models.XML;
    using DataImport.Tools;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    public class WorkoutDetailParser
    {
        public static void LoadWorkoutData(string xmlFileName, Workout workout)
        {
            if (!File.Exists(xmlFileName))
            {
                throw new ArgumentException($"File \"{xmlFileName}\" does not exist!");
            }

            if (workout == null)
            {
                throw new ArgumentNullException("workout");
            }

            using (FileStream xmlFile = new FileStream(xmlFileName, FileMode.Open, FileAccess.Read))
            {
                LoadWorkoutData(xmlFile, workout);
            }
        }

        public static void LoadWorkoutData(Stream stream, Workout workout)
        {
            var xmlSerializer = new XmlSerializer(typeof(WorkoutXml));
            var workoutXml = (WorkoutXml)xmlSerializer.Deserialize(stream);
            workout.Duration = workoutXml.Duration; // already loaded, but accuracu seems to be better here
            TransformWorkoutIntervals(workoutXml, workout);
        }

        private static void TransformWorkoutIntervals(WorkoutXml workoutXml, Workout workout)
        {
            var intervals = new List<WorkoutInterval>();
            foreach (var intervalXml in workoutXml.Intervals)
            {
                var interval = TransformWorkoutInterval(intervalXml);
                intervals.Add(interval);
            }

            workout.Intervals = intervals;
        }

        private static WorkoutInterval TransformWorkoutInterval(WorkoutIntervalXml intervalXml)
        {
            var lengths = new List<WorkoutIntervalLength>();
            foreach (var lengthXml in intervalXml.Lengths)
            {
                var length = TransformWorkoutIntervalLength(lengthXml);
                lengths.Add(length);
            }

            var interval = new WorkoutInterval
            {
                Lengths = lengths,
                TimeOffset = intervalXml.TimeOffset
            };

            return interval;
        }

        private static WorkoutIntervalLength TransformWorkoutIntervalLength(WorkoutIntervalLengthXml lengthXml)
        {
            var length = new WorkoutIntervalLength
            {
                Distance = lengthXml.Distance,
                Duration = lengthXml.Duration,
                StrokeCount = lengthXml.StrokeCount,
                StrokeTypeId = Converters.IntToStrokeType(lengthXml.StrokeType)
            };

            return length;
        }
    }
}
