namespace DataImport
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using DataImport.Models.XML;
    using DataImport.Tools;
    using DataModels;

    public static class WorkoutDetailParser
    {
        public static bool LoadWorkoutData(string xmlFileName, Workout workout)
        {
            if (!File.Exists(xmlFileName))
            {
                throw new ArgumentException($"File \"{xmlFileName}\" does not exist!");
            }

            if (workout == null)
            {
                throw new ArgumentNullException(nameof(workout));
            }

            using FileStream xmlFile = new FileStream(xmlFileName, FileMode.Open, FileAccess.Read);
            var result = LoadWorkoutData(xmlFile, workout);
            return result;
        }

        public static bool LoadWorkoutData(Stream stream, Workout workout)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (workout == null)
            {
                throw new ArgumentNullException(nameof(workout));
            }

            var xmlSerializer = new XmlSerializer(typeof(WorkoutXml));
            stream.Position = 0;
            using var xmlReader = XmlReader.Create(stream, new XmlReaderSettings { XmlResolver = null });
            var workoutXml = (WorkoutXml)xmlSerializer.Deserialize(xmlReader);

            workout.Duration = workoutXml.Duration; // already loaded, but accuracy seems to be better here
            workout.Start = workoutXml.Start;
            workout.WorkoutDate = workout.Start.Date;
            workout.Note = workoutXml.Note;
            workout.CourseLength = workoutXml.Course.CourseLength;

            var result = TransformWorkoutIntervals(workoutXml, workout);
            return result;
        }

        private static bool TransformWorkoutIntervals(WorkoutXml workoutXml, Workout workout)
        {
            if (workoutXml.Intervals == null)
            {
                return false;
            }

            //var intervals = new List<WorkoutInterval>();
            for (int i = 0; i < workoutXml.Intervals.Length; ++i)
            {
                var intervalXml = workoutXml.Intervals[i];
                var interval = TransformWorkoutInterval(intervalXml);
                if (interval != null)
                {
                    interval.IntervalNo = i + 1;
                    //intervals.Add(interval);
                    workout.Intervals.Add(interval);
                }
            }

            //workout.Intervals = intervals;
            workout.Distance = workout.Intervals.Sum(i => i.Distance);
            workout.ActiveTime = Math.Round(workout.Intervals.Sum(i => i.Duration), 1);
            workout.Pace = workout.Distance > 0 ? Math.Round(workout.ActiveTime * 100 / workout.Distance, 1) : 0;
            return true;
        }

        private static WorkoutInterval TransformWorkoutInterval(WorkoutIntervalXml intervalXml)
        {
            var interval = new WorkoutInterval
            {
                //Lengths = lengths,
                TimeOffset = intervalXml.TimeOffset
            };
            //var lengths = new List<WorkoutIntervalLength>();
            for (int i = 0; i < intervalXml.Lengths?.Length; ++i)
            {
                var lengthXml = intervalXml.Lengths[i];
                var length = TransformWorkoutIntervalLength(lengthXml);
                length.LengthNo = i + 1;
                interval.Lengths.Add(length);
                //lengths.Add(length);
            }

            //interval.Lengths.AddRange(lengths);

            if (interval.Lengths.Count > 0)
            {
                interval.Distance = interval.Lengths.Sum(l => l.Distance);
                interval.Duration = Math.Round(interval.Lengths.Sum(l => l.Duration), 1);
                interval.StrokeCount = interval.Lengths.Sum(l => l.StrokeCount) / (double)interval.Lengths.Count;
            }

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
