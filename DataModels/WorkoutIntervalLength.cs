namespace DataModels
{
    using Contracts.Entities;
    using DataTemplates.Entities;
    using static Constants.Enums;

    public class WorkoutIntervalLength : Entity, IWorkoutIntervalLength
    {
        public int WorkoutIntervalId { get; set; }

        public int? LengthNo { get; set; }

        public double Duration { get; set; }

        public StrokeType StrokeTypeId { get; set; }

        public int StrokeCount { get; set; }

        public double Distance { get; set; }

        public virtual WorkoutInterval WorkoutInterval { get; set; }
    }
}
