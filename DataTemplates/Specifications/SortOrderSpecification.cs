namespace NP.DataTemplates.Specifications
{
    using System;
    using System.Linq.Expressions;
    using NP.DataTemplates.Entities;
    using NP.DataTemplates.Interfaces;

    public class SortOrderSpecification<TEntity> : ISortOrderSpecification<TEntity> 
        where TEntity : Entity
    {
        public SortOrderSpecification(Expression<Func<TEntity, object>> sortSelector, bool sortDescending)
        {
            SortExpression = sortSelector;
            SortDescending = sortDescending;
        }

        public Expression<Func<TEntity, object>> SortExpression { get; }

        public bool SortDescending { get; }
    }
}
