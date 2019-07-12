namespace DataAccess.Models
{
    using Contracts.Entities;
    using DataTemplates.Entities;
    using System.ComponentModel.DataAnnotations;

    public class WorkoutIntervalType : Entity, IWorkoutIntervalType
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
