namespace Contracts.Entities
{
    using DataTemplates.Interfaces;

    public interface IWorkoutInterval : IEntity
    {
        float Distance { get; }
        float Duration { get; }
        //ICollection<IWorkoutIntervalLength> Lengths { get; set; }
        string Notes { get; set; }
        float TimeOffset { get; set; }
        //IWorkout Workout { get; set; }
        int WorkoutId { get; set; }
        //IWorkoutIntervalType WorkoutIntervalType { get; set; }
        int? WorkoutIntervalTypeId { get; set; }
        float StrokeCount { get; set; }
    }
}