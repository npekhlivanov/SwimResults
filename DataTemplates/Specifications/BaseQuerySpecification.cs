namespace NP.DataTemplates.Specifications
{
    using System;
    using System.Linq.Expressions;
    using NP.DataTemplates.Entities;
    using NP.DataTemplates.Interfaces;

    public class BaseQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : Entity
    {
        public BaseQuerySpecification(
            Expression<Func<TEntity, bool>> whereExpression,
            IIncludeSpecification<TEntity> includeSpecification,
            ISortOrderSpecification<TEntity> sortOrderSpecification,
            IPagingSpecification pagingSpecification)
        {
            WhereExpression = whereExpression;
            IncludeSpecification = includeSpecification;
            SortOrderSpecification = sortOrderSpecification;
            PagingSpecification = pagingSpecification;
        }

        public Expression<Func<TEntity, bool>> WhereExpression { get; }

        public IIncludeSpecification<TEntity> IncludeSpecification { get; }

        public ISortOrderSpecification<TEntity> SortOrderSpecification { get; }

        public IPagingSpecification PagingSpecification { get; }
    }
}
