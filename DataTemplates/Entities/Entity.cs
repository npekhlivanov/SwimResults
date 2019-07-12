namespace DataTemplates.Entities
{
    using DataTemplates.Interfaces;

    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
