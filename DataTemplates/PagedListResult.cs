namespace DataTemplates
{
    using DataTemplates.Entities;
    using System.Collections.Generic;

    public class PagedListResult<TEntity>
        where TEntity : Entity
    {
        public IList<TEntity> List { get; set; }
        public int TotalPages { get; set; }
    }
}
