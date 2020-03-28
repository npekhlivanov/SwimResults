namespace NP.DataTemplates.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using NP.DataTemplates.Entities;
    using NP.DataTemplates.Interfaces;

    public class BaseSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : Entity
    {
        public BaseSpecification(Expression<Func<TEntity, bool>> criteria, ISortOrderSpecification<TEntity> sortOrder, IPagingSpecification paging)
        {
            Criteria = criteria;
            Includes = new List<Expression<Func<TEntity, object>>>();
            SortOrder = sortOrder;
            PagingSpecification = paging;
        }

        public Expression<Func<TEntity, bool>> Criteria { get; }

        public IList<Expression<Func<TEntity, object>>> Includes { get; }

        public ISortOrderSpecification<TEntity> SortOrder { get; }

        public IPagingSpecification PagingSpecification { get; } 

        protected virtual void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
