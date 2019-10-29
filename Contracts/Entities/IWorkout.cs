namespace Contracts.Entities
{
    using System;
    using DataTemplates.Interfaces;

    public interface IWorkout : IEntity
    {
        DateTime WorkoutDate { get; set; }
        float Distance { get; set; }
        float Duration { get; set; }
        //ICollection<IWorkoutInterval> Intervals { get; set; }
        string Name { get; set; }
        string Note { get; set; }
        float Pace { get; set; }
        string Place { get; set; }
        DateTime Start { get; set; }
    }
}