namespace DataTemplates.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using DataTemplates.Entities;
    using DataTemplates.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public abstract class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : Entity
    {
        internal readonly DbContext _context;

        protected ReadOnlyRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual async Task<IList<TEntity>> GetList()
        {
            var result = await _context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
            return result;
        }

        private IQueryable<TEntity> GetListAsQueriable<TKey>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending)
        {
            var dbSet = _context.Set<TEntity>();
            IQueryable<TEntity> result;
            if (sortDescending)
            {
                result = dbSet.OrderByDescending(sortSelector);
            }
            else
            {
                result = dbSet.OrderBy(sortSelector);
            }

            return result;
        }

        private void ValidatePageParams(int pageSize, int pageNo)
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

        public virtual async Task<IList<TEntity>> GetList<TKey>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending)
        {
            var result = GetListAsQueriable(sortSelector, sortDescending);
            return await result.AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public virtual async Task<IList<TEntity>> GetList<TKey>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo)
        {
            ValidatePageParams(pageSize, pageNo);

            var result = GetListAsQueriable(sortSelector, sortDescending);
            result = result.Skip(pageNo * pageSize)
                .Take(pageSize);
            return await result.AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public virtual async Task<IList<TEntity>> GetList<TKey, TProperty>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo,
            Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            ValidatePageParams(pageSize, pageNo);

            var result = GetListAsQueriable(sortSelector, sortDescending);
            result = result.Skip(pageNo * pageSize)
                .Take(pageSize)
                .Include(navigationPropertyPath);
            return await result.AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }


        public virtual async Task<IList<TEntity>> GetList<TKey, TProperty, TProperty2>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo,
            Expression<Func<TEntity, TProperty>> navigationPropertyPath, Expression<Func<TEntity, TProperty2>> otherNavigationPropertyPath)
        {
            ValidatePageParams(pageSize, pageNo);

            var result = GetListAsQueriable(sortSelector, sortDescending);
            result = result.Skip(pageNo * pageSize)
                .Take(pageSize)
                .Include(navigationPropertyPath)
                .Include(otherNavigationPropertyPath);
            return await result.AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public virtual async Task<IList<TEntity>> GetList<TKey, TProperty, TProperty2>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> sortSelector,
            bool sortDescending, int pageSize, int pageNo, Expression<Func<TEntity, TProperty>> navigationPropertyPath, Expression<Func<TEntity, TProperty2>> otherNavigationPropertyPath)
        {
            ValidatePageParams(pageSize, pageNo);

            var result = GetListAsQueriable(sortSelector, sortDescending)
                .Where(predicate);
            result = result.Skip(pageNo * pageSize)
                .Take(pageSize)
                .Include(navigationPropertyPath)
                .Include(otherNavigationPropertyPath);
            return await result.AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public virtual async Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            //var result = Task.Factory.StartNew<IList<TEntity>>(() => Expression<Func<TEntity, TProperty>> navigationPropertyPath
            //{
            //    IQueryable<TEntity> entities = _context.Set<TEntity>().AsQueryable();
            //    return entities.Where(predicate).ToList();
            //});
            var result = await _context.Set<TEntity>()
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
            return result;
        }

        public virtual async Task<IList<TEntity>> GetList<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            var result = await _context.Set<TEntity>()
                .Include(navigationPropertyPath)
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
            return result;
        }

        public async Task<TEntity> GetById(int id)
        {
            var result = await _context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id)
                .ConfigureAwait(false);
            return result;
        }

        public async Task<TEntity> GetById<TProperty>(int id, Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            var result = await _context.Set<TEntity>()
                .Include(navigationPropertyPath)
                .FirstOrDefaultAsync(e => e.Id == id)
                .ConfigureAwait(false);
            return result;
        }

        public async Task<TEntity> GetById<TProperty1, TProperty2>(int id, Expression<Func<TEntity, TProperty1>> navigationProperty1Path, Expression<Func<TProperty1, TProperty2>> NavigationProperty2Path)
        {
            var result = await _context.Set<TEntity>()
                .Include(navigationProperty1Path)
                .ThenInclude(NavigationProperty2Path)
                .FirstOrDefaultAsync(e => e.Id == id)
                .ConfigureAwait(false);
            return result;
        }

        public async Task<TEntity> GetById<TProperty1, TProperty2>(int id, Expression<Func<TEntity, TProperty1>> navigationProperty1Path,
            Expression<Func<TEntity, TProperty2>> navigationProperty2Path)
        {
            var result = await _context.Set<TEntity>()
                .Include(navigationProperty1Path)
                .Include(navigationProperty2Path)
                .FirstOrDefaultAsync(e => e.Id == id)
                .ConfigureAwait(false);
            return result;
        }

        public async Task<TEntity> GetById<TProperty1, TProperty2, TProperty3>(int id, Expression<Func<TEntity, TProperty1>> navigationProperty1Path,
                Expression<Func<TEntity, TProperty2>> navigationProperty2Path, Expression<Func<TEntity, TProperty3>> navigationProperty3Path)
        {
            var result = await _context.Set<TEntity>()
                .Include(navigationProperty1Path)
                .Include(navigationProperty2Path)
                .Include(navigationProperty3Path)
                .FirstOrDefaultAsync(e => e.Id == id)
                .ConfigureAwait(false);
            return result;
        }

        public async Task<int> GetCount()
        {
            var result = await _context.Set<TEntity>()
                .CountAsync()
                .ConfigureAwait(false);
            return result;
        }

        public async Task<int> GetCount(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await _context.Set<TEntity>()
                .Where(predicate)
                .CountAsync()
                .ConfigureAwait(false);
            return result;
        }
    }

}
