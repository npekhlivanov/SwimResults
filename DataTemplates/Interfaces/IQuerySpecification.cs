namespace NP.DataTemplates.Interfaces
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Interface to a QuerySpecification object, used to specify quiery parameters
    /// </summary>
    /// <seealso cref="https://deviq.com/specification-pattern/"/>
    /// <typeparam name="TEntity">Interface to an Entity object</typeparam>
    public interface IQuerySpecification<TEntity>
        where TEntity : IEntity
    {
        Expression<Func<TEntity, bool>> WhereExpression { get; }

        IIncludeSpecification<TEntity> IncludeSpecification { get; }

        ISortOrderSpecification<TEntity> SortOrderSpecification { get; }

        IPagingSpecification PagingSpecification { get; }
    }
}
