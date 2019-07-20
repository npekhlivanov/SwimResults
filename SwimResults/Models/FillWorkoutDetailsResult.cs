using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwimResults.Models
{
    public class FillWorkoutDetailsResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public WorkoutDetailsData WorkoutData { get; set; }
    }

    public class WorkoutDetailsData
    {
        public int WorkoutId { get; set; }
        public string Name { get; set; }
        public string Distance { get; set; }
        public string Duration { get; set; }
        public string Pace { get; set; }
        public string StartDate { get; set; }
    }
}
