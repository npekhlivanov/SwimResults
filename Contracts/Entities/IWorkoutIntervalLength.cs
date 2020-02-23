namespace Contracts.Entities
{
    using Constants;
    using DataTemplates.Interfaces;

    public interface IWorkoutIntervalLength : IEntity
    {
        double Distance { get; set; }
        double Duration { get; set; }
        int StrokeCount { get; set; }
        Enums.StrokeType StrokeTypeId { get; set; }
        //IWorkoutInterval WorkoutInterval { get; set; }
        int WorkoutIntervalId { get; set; }
    }
}