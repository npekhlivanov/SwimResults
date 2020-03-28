namespace Contracts.Entities
{
    using Constants.Enums;
    using NP.DataTemplates.Interfaces;

    public interface IWorkoutIntervalLength : IEntity
    {
        double Distance { get; set; }
        double Duration { get; set; }
        int StrokeCount { get; set; }
        StrokeType StrokeTypeId { get; set; }
        //IWorkoutInterval WorkoutInterval { get; set; }
        int WorkoutIntervalId { get; set; }
    }
}