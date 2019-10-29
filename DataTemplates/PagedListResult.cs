namespace DataTemplates
{
    using System.Collections.Generic;
    using DataTemplates.Entities;

    public class PagedListResult<TEntity>
        where TEntity : Entity
    {
        public IList<TEntity> List { get; set; }
        public int TotalPages { get; set; }
    }
}
