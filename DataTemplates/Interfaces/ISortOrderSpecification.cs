namespace NP.DataTemplates.Interfaces
{
    using System;
    using System.Linq.Expressions;

    public interface ISortOrderSpecification<TEntity>
        where TEntity : IEntity
    {
        Expression<Func<TEntity, object>> SortExpression { get; }

        bool SortDescending { get; }
    }
}