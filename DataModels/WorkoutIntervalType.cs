namespace DataModels
{
    using System.ComponentModel.DataAnnotations;
    using Contracts.Entities;
    using NP.DataTemplates.Entities;

    public class WorkoutIntervalType : Entity, IWorkoutIntervalType
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
