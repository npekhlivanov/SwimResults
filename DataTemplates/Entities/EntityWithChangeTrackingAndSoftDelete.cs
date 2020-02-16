namespace DataTemplates.Entities
{
    using System;

    public class EntityWithChangeTrackingAndSoftDelete : EntityWithChangeTracking
    {
        public DateTime DeletedOn { get; set; }

        public string DeletedBy { get; set; }
    }
}
