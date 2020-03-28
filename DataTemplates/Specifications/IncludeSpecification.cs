namespace NP.DataTemplates.Specifications
{
    using System;
    using System.Linq;
    using NP.DataTemplates.Entities;
    using NP.DataTemplates.Interfaces;
    using NP.Helpers;

    public class IncludeSpecification<TEntity> : IIncludeSpecification<TEntity>
        where TEntity : Entity
    {
        public IncludeSpecification(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeDelegate)
        {
            IncludeDelegate = Validators.ValidateNotNull(includeDelegate, nameof(includeDelegate));
        }

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> IncludeDelegate { get; }
    }
}
