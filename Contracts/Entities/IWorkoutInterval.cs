namespace Contracts.Entities
{
    using DataTemplates.Interfaces;

    public interface IWorkoutInterval : IEntity
    {
        double Distance { get; }
        double Duration { get; }
        //ICollection<IWorkoutIntervalLength> Lengths { get; set; }
        string Notes { get; set; }
        double TimeOffset { get; set; }
        //IWorkout Workout { get; set; }
        int WorkoutId { get; set; }
        //IWorkoutIntervalType WorkoutIntervalType { get; set; }
        int? WorkoutIntervalTypeId { get; set; }
        double StrokeCount { get; set; }
    }
}