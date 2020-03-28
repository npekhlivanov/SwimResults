namespace Contracts.Entities
{
    using System;
    using NP.DataTemplates.Interfaces;

    public interface IWorkout : IEntity
    {
        DateTime WorkoutDate { get; set; }
        double Distance { get; set; }
        double Duration { get; set; }
        //ICollection<IWorkoutInterval> Intervals { get; set; }
        string Name { get; set; }
        string Note { get; set; }
        double Pace { get; set; }
        string Place { get; set; }
        DateTime Start { get; set; }
    }
}