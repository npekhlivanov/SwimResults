namespace NP.DataTemplates.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface ISpecification<TEntity>
        where TEntity : IEntity
    {
        Expression<Func<TEntity, bool>> Criteria { get; }

        IList<Expression<Func<TEntity, object>>> Includes { get; }

        ISortOrderSpecification<TEntity> SortOrder { get; }

        IPagingSpecification PagingSpecification { get; }
    }
}
