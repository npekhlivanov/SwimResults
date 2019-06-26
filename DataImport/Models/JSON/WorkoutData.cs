namespace DataImport.Models.JSON
{
    public class WorkoutData
    {
        public int WorkoutId { get; set; }
        public string WorkoutName { get; set; }
        public float Distance { get; set; }
        public float Duration { get; set; }
        public float Pace { get; set; }
        public string WorkoutNote { get; set; }
        public string PlaceName => Place?.Name;

        public Place Place { get; set; }
        public Pool Pool { get; set; }
        public string WorkoutTitle { get; set; }
        public long WorkoutDate { get; set; }
        public Durationformat DurationFormat { get; set; }
        public int Achievement { get; set; }
        public Device Device { get; set; }
        public Paceformat PaceFormat { get; set; }
        public string Unit { get; set; }
        public bool Manual { get; set; }
        public object ManualIcon { get; set; }
        public string DistanceFormated { get; set; }
        public int PaceForSort { get; set; }
    }
}
