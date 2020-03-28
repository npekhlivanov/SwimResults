namespace NP.DataTemplates.Entities
{
    using System;

    public class EntityWithChangeTracking : Entity
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
    }
}
