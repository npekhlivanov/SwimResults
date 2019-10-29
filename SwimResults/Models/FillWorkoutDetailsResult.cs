namespace SwimResults.Models
{
    public class FillWorkoutDetailsResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public WorkoutDetailsData WorkoutData { get; set; }
    }
}
