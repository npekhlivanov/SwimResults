namespace DataTemplates
{
    using System.Collections.Generic;
    using DataTemplates.Entities;

    public class PagedListResult<TEntity>
        where TEntity : Entity
    {
        public PagedListResult()
        {
            this.List = new List<TEntity>();
        }

        public IList<TEntity> List { get; }

        public int TotalPages { get; set; }
    }
}
