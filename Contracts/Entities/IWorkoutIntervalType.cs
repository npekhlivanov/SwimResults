namespace Contracts.Entities
{
    using NP.DataTemplates.Interfaces;

    public interface IWorkoutIntervalType : IEntity
    {
        string Name { get; set; }
    }
}