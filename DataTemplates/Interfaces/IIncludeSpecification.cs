namespace NP.DataTemplates.Interfaces
{
    using System;
    using System.Linq;

    public interface IIncludeSpecification<TEntity>
        where TEntity : IEntity
    {
        Func<IQueryable<TEntity>, IQueryable<TEntity>> IncludeDelegate { get; }
    }
}