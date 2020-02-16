namespace DataTemplates.Entities
{
    public class EntityWithSoftDelete : Entity
    {
        public bool DeletedOn { get; set; }
    }
}
