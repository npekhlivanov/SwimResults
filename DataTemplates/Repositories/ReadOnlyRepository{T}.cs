namespace NP.DataTemplates.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NP.DataTemplates.Entities;
    using NP.DataTemplates.Interfaces;
    using NP.Helpers;

    public abstract class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : Entity
    {
        internal readonly DbContext _context;
        internal readonly DbSet<TEntity> _dbSet;

        protected ReadOnlyRepository(DbContext context)
        {
            _context = context.ValidateNotNull(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        #region Private methods
        private static void ValidatePageParams(int pageSize, int pageNo)
        {
            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            if (pageNo < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNo));
            }
        }

        private static Task<List<TEntity>> GetEntitiesAsList(IQueryable<TEntity> queriable)
        {
            return queriable
                .AsNoTracking()
                .ToListAsync();
        }

        private static IQueryable<TEntity> ApplyIncludeSpecification(IQueryable<TEntity> queriable, IIncludeSpecification<TEntity> includeSpecification)
        {
            return includeSpecification.IncludeDelegate(queriable);
        }

        private static IQueryable<TEntity> ApplyOrdering<TKey>(IQueryable<TEntity> queriable, Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending)
        {
            if (sortDescending)
            {
                return queriable.OrderByDescending(sortSelector);
            }
            else
            {
                return queriable.OrderBy(sortSelector);
            }
        }

        private static IQueryable<TEntity> ApplyPagingSpecification(IQueryable<TEntity> queriable, IPagingSpecification specification)
        {
            ValidatePageParams(specification.PageSize, specification.PageNo);
            var result = queriable
                .Skip(specification.PageNo * specification.PageSize)
                .Take(specification.PageSize);
            return result;
        }

        private IQueryable<TEntity> ApplySpecificationToDbSet(IQuerySpecification<TEntity> specification)
        {
            //var queryableResultWithIncludes = specification.Includes.Aggregate(_dbSet.AsQueryable(),
            //    (current, include) => current.Include(include));

            var result = _dbSet.AsQueryable();
            if (specification.IncludeSpecification != null)
            {
                result = ApplyIncludeSpecification(result, specification.IncludeSpecification);
            }

            if (specification.WhereExpression != null)
            {
                result = result.Where(specification.WhereExpression);
            }

            var sortOrder = specification.SortOrderSpecification;
            if (sortOrder != null)
            {
                result = ApplyOrdering(result, sortOrder.SortExpression, sortOrder.SortDescending);
            }

            var paging = specification.PagingSpecification;
            if (paging != null)
            {
                result = ApplyPagingSpecification(result, paging);
            }

            return result;
        }
        #endregion

        public virtual async Task<IList<TEntity>> GetList()
        {
            var result = await GetEntitiesAsList(_dbSet)
                .ConfigureAwait(false);
            return result;
        }

        public async Task<IList<TEntity>> GetList(IQuerySpecification<TEntity> specification)
        {
            Validators.ValidateNotNull(specification, nameof(specification));

            var queriable = ApplySpecificationToDbSet(specification);
            var result = await GetEntitiesAsList(queriable)
                .ConfigureAwait(false);
            return result;
        }

        //public virtual async Task<IList<TEntity>> GetList<TKey>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending)
        //{
        //    var queriable = ApplyOrdering(_dbSet, sortSelector, sortDescending);
        //    var result = await GetEntities(queriable)
        //        .ConfigureAwait(false);
        //    return result;
        //}

        //public virtual async Task<IList<TEntity>> GetList(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryModifier)
        //{
        //    queryModifier.ValidateNotNull(nameof(queryModifier));
        //    var queriable = queryModifier(_dbSet);
        //    var result = await GetEntities(queriable)
        //        .ConfigureAwait(false);
        //    return result;
        //}

        //public virtual async Task<IList<TEntity>> GetList(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryModifier, int pageSize, int pageNo)
        //{
        //    queryModifier.ValidateNotNull(nameof(queryModifier));
        //    var queriable = queryModifier(_dbSet);
        //    var result = await GetEntities(queriable, pageSize, pageNo)
        //        .ConfigureAwait(false);
        //    return result;
        //}

        //public virtual async Task<IList<TEntity>> GetList<TKey>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo)
        //{
        //    var queriable = ApplyOrdering(_dbSet, sortSelector, sortDescending);
        //    var result = await GetEntities(queriable, pageSize, pageNo)
        //        .ConfigureAwait(false);
        //    return result;
        //}

        //public virtual async Task<IList<TEntity>> GetList<TKey>(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryModifier, int pageSize, int pageNo,
        //    Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending)
        //{
        //    queryModifier.ValidateNotNull(nameof(queryModifier));
        //    var queriable = ApplyOrdering(_dbSet, sortSelector, sortDescending);
        //    queriable = queryModifier(queriable);
        //    var result = await GetEntities(queriable, pageSize, pageNo)
        //        .ConfigureAwait(false);
        //    return result;
        //}

        //public virtual async Task<IList<TEntity>> GetList<TKey, TProperty>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo,
        //    Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        //{
        //    ValidatePageParams(pageSize, pageNo);

        //    var queriable = GetListAsQueriable(sortSelector, sortDescending);
        //    return await queriable
        //        .Include(navigationPropertyPath)
        //        .Skip(pageNo * pageSize)
        //        .Take(pageSize)
        //        .AsNoTracking()
        //        .ToListAsync()
        //        .ConfigureAwait(false);
        //}

        //public virtual async Task<IList<TEntity>> GetList<TKey, TProperty, TProperty2>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo,
        //    Expression<Func<TEntity, TProperty>> navigationPropertyPath, Expression<Func<TEntity, TProperty2>> otherNavigationPropertyPath)
        //{
        //    ValidatePageParams(pageSize, pageNo);

        //    var queriable = GetListAsQueriable(sortSelector, sortDescending);
        //    return await queriable
        //        .Include(navigationPropertyPath)
        //        .Include(otherNavigationPropertyPath)
        //        .Skip(pageNo * pageSize)
        //        .Take(pageSize)
        //        .AsNoTracking()
        //        .ToListAsync()
        //        .ConfigureAwait(false);
        //}

        //public virtual async Task<IList<TEntity>> GetList<TKey, TProperty, TProperty2>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> sortSelector,
        //    bool sortDescending, int pageSize, int pageNo, Expression<Func<TEntity, TProperty>> navigationPropertyPath, Expression<Func<TEntity, TProperty2>> otherNavigationPropertyPath)
        //{
        //    ValidatePageParams(pageSize, pageNo);

        //    var queriable = GetListAsQueriable(sortSelector, sortDescending)
        //        .Where(predicate);
        //    return await queriable
        //        .Include(navigationPropertyPath)
        //        .Include(otherNavigationPropertyPath)
        //        .Skip(pageNo * pageSize)
        //        .Take(pageSize)
        //        .AsNoTracking()
        //        .ToListAsync()
        //        .ConfigureAwait(false);
        //}

        //public virtual async Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate)
        //{
        //    //var result = Task.Factory.StartNew<IList<TEntity>>(() => Expression<Func<TEntity, TProperty>> navigationPropertyPath
        //    //{
        //    //    IQueryable<TEntity> entities = _context.Set<TEntity>().AsQueryable();
        //    //    return entities.Where(predicate).ToList();
        //    //});
        //    var result = await _dbSet
        //        .Where(predicate)
        //        .AsNoTracking()
        //        .ToListAsync()
        //        .ConfigureAwait(false);
        //    return result;
        //}

        //public virtual async Task<IList<TEntity>> GetList<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        //{
        //    var result = await _dbSet
        //        .Include(navigationPropertyPath)
        //        .Where(predicate)
        //        .AsNoTracking()
        //        .ToListAsync()
        //        .ConfigureAwait(false);
        //    return result;
        //}

        public async Task<TEntity> GetById(int id)
        {
            var result = await _dbSet
                .FirstOrDefaultAsync(e => e.Id == id)
                .ConfigureAwait(false);
            return result;
        }

        public async Task<TEntity> GetById(int id, IIncludeSpecification<TEntity> specification)
        {
            specification.ValidateNotNull(nameof(specification));

            var queriable = ApplyIncludeSpecification(_dbSet, specification);
            var result = await queriable
                .FirstOrDefaultAsync(e => e.Id == id)
                .ConfigureAwait(false);
            return result;
        }

        //public async Task<TEntity> GetById(int id, [NotNull] Func<IQueryable<TEntity>, IQueryable<TEntity>> queryModifier)
        //{
        //    queryModifier.ValidateNotNull(nameof(queryModifier));
        //    var queriable = queryModifier(_dbSet);
        //    var result = await queriable
        //        .FirstOrDefaultAsync(e => e.Id == id)
        //        .ConfigureAwait(false);
        //    return result;
        //}

        //public async Task<TEntity> GetById<TProperty>(int id, Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        //{
        //    var result = await _dbSet
        //        .Include(navigationPropertyPath)
        //        .FirstOrDefaultAsync(e => e.Id == id)
        //        .ConfigureAwait(false);
        //    return result;
        //}

        //public async Task<TEntity> GetById<TProperty1, TProperty2>(int id, Expression<Func<TEntity, TProperty1>> navigationProperty1Path, Expression<Func<TProperty1, TProperty2>> NavigationProperty2Path)
        //{
        //    var result = await _dbSet
        //        .Include(navigationProperty1Path)
        //        .ThenInclude(NavigationProperty2Path)
        //        .FirstOrDefaultAsync(e => e.Id == id)
        //        .ConfigureAwait(false);
        //    return result;
        //}

        //public async Task<TEntity> GetById<TProperty1, TProperty2>(int id, Expression<Func<TEntity, TProperty1>> navigationProperty1Path,
        //    Expression<Func<TEntity, TProperty2>> navigationProperty2Path)
        //{
        //    var result = await _dbSet
        //        .Include(navigationProperty1Path)
        //        .Include(navigationProperty2Path)
        //        .FirstOrDefaultAsync(e => e.Id == id)
        //        .ConfigureAwait(false);
        //    return result;
        //}

        //public async Task<TEntity> GetById<TProperty1, TProperty2, TProperty3>(int id, Expression<Func<TEntity, TProperty1>> navigationProperty1Path,
        //    Expression<Func<TEntity, TProperty2>> navigationProperty2Path, Expression<Func<TEntity, TProperty3>> navigationProperty3Path)
        //{
        //    var result = await _context.Set<TEntity>()
        //        .Include(navigationProperty1Path)
        //        .Include(navigationProperty2Path)
        //        .Include(navigationProperty3Path)
        //        .FirstOrDefaultAsync(e => e.Id == id)
        //        .ConfigureAwait(false);
        //    return result;
        //}

        public async Task<int> GetCount()
        {
            var result = await _dbSet
                .CountAsync()
                .ConfigureAwait(false);
            return result;
        }

        public async Task<int> GetCount(IQuerySpecification<TEntity> specification) //Expression<Func<TEntity, bool>> predicate)
        {
            Validators.ValidateNotNull(specification, nameof(specification));

            var result = await ApplySpecificationToDbSet(specification)
            //var result = await _dbSet
            //    .Where(predicate)
                .CountAsync()
                .ConfigureAwait(false);
            return result;
        }
    }
}
