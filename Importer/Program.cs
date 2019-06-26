namespace Importer
{
    using DataImport;
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var workouts = WorkoutListParser.LoadWorkoutList(@"T:\OneDrive\Архив\Плуване\AllSwims.json"); //"C:\\Temp\\Workouts.json"
            Console.WriteLine($"Workouts loaded: {workouts?.Count ?? 0}");
            var myWorkout = workouts.Where(w => w.Id == 1248785).FirstOrDefault();
            //var xmlDataStream = WorkoutDetailsRetriever.DownloadWorkoutDetails("https://www.swim.com/export/xml/", 1248785).Result;
            //WorkoutDetailParser.LoadWorkoutData(xmlDataStream, myWorkout);
            WorkoutDetailParser.LoadWorkoutData(@"T:\OneDrive\Архив\Плуване\1248785.xml", myWorkout);
        }
    }
}
