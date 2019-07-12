namespace Contracts.Entities
{
    using DataTemplates.Interfaces;

    public interface IWorkoutIntervalType : IEntity
    {
        string Name { get; set; }
    }
}