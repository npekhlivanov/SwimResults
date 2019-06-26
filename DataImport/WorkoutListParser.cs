namespace DataImport
{
    using DataAccess.Models;
    using DataImport.Models.JSON;
    using DataImport.Tools;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;

    public class WorkoutListParser
    {
        public static ICollection<Workout> LoadWorkoutList(string jsonFileName)
        {
            using (StreamReader reader = new StreamReader(jsonFileName))
            {
                var jsonData = reader.ReadToEnd();
                var root = JsonConvert.DeserializeObject<RootObject>(jsonData);

                var feedList = root?.Result?.FeedList;
                if (feedList == null)
                {
                    return null;
                }

                var workouts = TransformFeedList(feedList);
                return workouts;
            }
        }

        private static List<Workout> TransformFeedList(FeedListItem[] feedList)
        {
            var workouts = new List<Workout>();
            foreach (var item in feedList)
            {
                var workout = ParseFeedListItem(item);
                workouts.Add(workout);
            }

            return workouts;
        }

        private static Workout ParseFeedListItem(FeedListItem item)
        {
            var workoutDate = Converters.UnixTimeToDateTime(item.WorkoutDate);
            var workout = new Workout
            {
                Id = item.WorkoutId,
                Name = item.WorkoutTitle,
                Distance = item.WorkoutData != null ? item.WorkoutData.Distance : 0,
                Duration = item.WorkoutData?.Duration ?? 0,
                Start = workoutDate,
                Date = workoutDate.Date,
                Note = item.WorkoutData?.WorkoutNote,
                Place = item.WorkoutData?.PlaceName,
                Pace = item.WorkoutData?.Pace ?? 0,
            };
            return workout;
        }
    }
}
