namespace Contracts.Entities
{
    using Constants;
    using DataTemplates.Interfaces;

    public interface IWorkoutIntervalLength : IEntity
    {
        float Distance { get; set; }
        float Duration { get; set; }
        int StrokeCount { get; set; }
        Enums.StrokeType StrokeTypeId { get; set; }
        //IWorkoutInterval WorkoutInterval { get; set; }
        int WorkoutIntervalId { get; set; }
    }
}