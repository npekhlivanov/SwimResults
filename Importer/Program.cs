﻿namespace Importer
{
    using DataAccess.Data;
    using DataAccess.Models;
    using DataImport;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumExtensions
    {
        public static int GetMaxValue(this Enum enumType)
        {
            var enumValues = Enum.GetValues(enumType.GetType());
            var enumValList = new List<int>((int[])enumValues);
            var maxVal = enumValList.Sum();
            return maxVal;
        }

        public static int GetMaxValue(this Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException($"Type \"{enumType.Name}\" is not enum!");
            }

            var enumValues = Enum.GetValues(enumType);
            var listVals = enumValues.Cast<List<int>>();
            var lastVal = (int)enumValues.GetValue(enumValues.Length - 1);
            var maxVal = (lastVal << 1) - 1;
            return maxVal;
        }

        public static T IntToFlags<T>(int value) where T : Enum
        {
            var typeOfT = typeof(T);
            if (!typeOfT.IsEnum || !typeOfT.IsDefined(typeof(FlagsAttribute), false))
            {
                throw new ArgumentException($"Type \"{typeOfT.Name}\" is not Enum with Flags attribute!");
            }

            //var enumT = enumType.GetType();
            var enumValues = Enum.GetValues(typeOfT);
            var lastVal = (int)enumValues.GetValue(enumValues.Length - 1);
            var maxVal = (lastVal << 1) - 1;
            if (value > maxVal)
            {
                throw new ArgumentOutOfRangeException("value", $"Value \"{value}\" exceeds limit for enum \"{typeOfT.Name}\"");
            }

            Enum.TryParse(typeOfT, value.ToString(), out object result);
            return (T)result;
        }
    }

    class Program
    {
        [Flags]
        public enum TestFlags1
        {
            Flag1 = 1,
            Flag2 = 2,
            Flag3 = 4
        }

        public enum TestEnum1
        {
            Value1, Value2, Value3
        }

        static void Main(string[] args)
        {

            var enumType = Enum.GetUnderlyingType(typeof(TestFlags1));
            var enumType2 = Enum.GetUnderlyingType(typeof(TestEnum1));
            Console.WriteLine($"value1={(int)TestEnum1.Value1},value3={(int)TestEnum1.Value3}");
            //var enumValues1 = Enum.GetValues(typeof(TestFlags1));
            //var enumValList = new List<int>((int[])enumValues1); 
            //int maxVal = TestEnum1.GetMaxValue(typeof(TestEnum1)); // flags.GetMaxValue();// enumValList.Sum(); 
            TestFlags1 fl1 = EnumExtensions.IntToFlags<TestFlags1>(7);
            var maxVal = typeof(TestFlags1).GetMaxValue();

            var workoutId = 0;
            if (args.Length == 1)
            {
                if (!int.TryParse(args[0], out workoutId) || workoutId < 1)
                {
                    Console.WriteLine("A valid WorkoutId mus be specified!");
                    return;
                }
            }

            var loadedWorkouts = WorkoutListParser.LoadWorkoutList(@"H:\OneDrive\Архив\Плуване\AllSwims.json"); //"C:\\Temp\\Workouts.json"
            Console.WriteLine($"Workouts loaded: {loadedWorkouts?.Count ?? 0}");

            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            using (var dbContext = new SqlServerDbContext(connectionString))
            {
                var repository = new WorkoutRepository(dbContext);

                var storedWorkouts = repository.GetList(w => w.Start, true).Result;
                var newWorkouts = GetNewWorkouts(loadedWorkouts, storedWorkouts);
                SaveWorkouts(newWorkouts, repository);

                var myWorkout = storedWorkouts.Where(w => w.Id == workoutId).FirstOrDefault();
                if (myWorkout == null)
                {
                    myWorkout = loadedWorkouts.Where(w => w.Id == workoutId).FirstOrDefault();
                    if (myWorkout == null)
                    {
                        Console.WriteLine($"Workout with id {workoutId} is not found!");
                        return;
                    }
                }

                //var xmlDataStream = WorkoutDetailsRetriever.DownloadWorkoutDetails("https://www.swim.com/export/xml/", 1248785).Result;
                //WorkoutDetailParser.LoadWorkoutData(xmlDataStream, myWorkout);

                WorkoutDetailParser.LoadWorkoutData($@"H:\OneDrive\Архив\Плуване\{workoutId}.xml", myWorkout);

                //var workoutInDb = repository.Get(workoutId).Result;
                var workoutInDb = repository.GetList(w => w.Id == workoutId).Result;
                if (workoutInDb == null)
                {
                    var id = repository.Add(myWorkout).Result;
                }
                else
                {
                    //var updated = repository.UpdateModifiedFields(myWorkout).Result;
                    repository.Update(myWorkout).Wait();
                }

                repository.FindAndDelete(workoutId).Wait();
            }
        }

        static void SaveWorkouts(IList<Workout> workouts, WorkoutRepository repository)
        {
            var sortedWorkouts = workouts.OrderBy(w => w.Start).ToList();
            for (int i = 0; i < sortedWorkouts.Count(); ++i)
            {
                var workout = sortedWorkouts[i];
                repository.Add(workout).Wait();
            }
        }

        static IList<Workout> GetNewWorkouts(IList<Workout> loadedWorkouts, IList<Workout> storedWorkouts)
        {
            //var result = new List<Workout>();
            //foreach (var workout in loadedWorkouts)
            //{
            //    if (!storedWorkouts.Any(w => w.Id == workout.Id))
            //    {
            //        result.Add(workout);
            //    }
            //}

            //return result;

            var result = loadedWorkouts.Except(storedWorkouts, new WorkoutComparer());
            return result.ToList();
        }

        class WorkoutComparer : IEqualityComparer<Workout>
        {
            public bool Equals(Workout x, Workout y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Workout obj)
            {
                return obj?.Id.GetHashCode() ?? 0;
            }
        }
    }
}