namespace NP.DataTemplates.Entities
{
    using NP.DataTemplates.Interfaces;

    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
