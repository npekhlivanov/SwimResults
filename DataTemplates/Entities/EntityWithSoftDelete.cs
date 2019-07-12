namespace DataTemplates.Entities
{
    class EntityWithSoftDelete : Entity
    {
        public bool DeletedOn { get; set; }
    }
}
