﻿namespace DataTemplates.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using System;
    using DataTemplates.Entities;
    using System.Linq.Expressions;
    using DataTemplates.Interfaces;

    public abstract class ReadOnlyRepository<TEntity> : IReadOnyReposotory<TEntity>
        where TEntity : Entity
    {
        protected readonly DbContext _context;

        public ReadOnlyRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(DbContext));
        }

        public virtual async Task<IList<TEntity>> GetList()
        {
            var result = _context.Set<TEntity>().ToListAsync(); //.AsNoTracking() ?
            return await result;
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

        public virtual async Task<IList<TEntity>> GetList<TKey>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending)
        {
            var result = GetListAsQueriable(sortSelector, sortDescending);
            return await result.ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetList<TKey>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo)
        {
            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize");
            }

            if (pageNo < 0)
            {
                throw new ArgumentOutOfRangeException("pageNo");
            }

            var result = GetListAsQueriable(sortSelector, sortDescending);
            result = result.Skip(pageNo * pageSize).Take(pageSize);
            return await result.ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            //var result = Task.Factory.StartNew<IList<TEntity>>(() => Expression<Func<TEntity, TProperty>> navigationPropertyPath
            //{
            //    IQueryable<TEntity> entities = _context.Set<TEntity>().AsQueryable();
            //    return entities.Where(predicate).ToList();
            //});
            var result = _context.Set<TEntity>()
                .Where(predicate)
                .ToListAsync();
            return await result;
        }

        public virtual async Task<IList<TEntity>> GetList<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            var result = _context.Set<TEntity>()
                .Include(navigationPropertyPath)
                .Where(predicate)
                .ToListAsync();
            return await result;
        }

        public async Task<TEntity> Get(int id)
        {
            var result = _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            return await result;
        }

        public async Task<TEntity> Get<TProperty>(int id, Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            var result = _context.Set<TEntity>()
                .Include(navigationPropertyPath)
                .FirstOrDefaultAsync(e => e.Id == id);
            return await result;
        }

        public async Task<TEntity> Get<TProperty1, TProperty2>(int id, Expression<Func<TEntity, TProperty1>> navigationProperty1Path, Expression<Func<TProperty1, TProperty2>> NavigationProperty2Path)
        {
            var result = _context.Set<TEntity>()
                .Include(navigationProperty1Path)
                .ThenInclude(NavigationProperty2Path)
                .FirstOrDefaultAsync(e => e.Id == id);
            return await result;
        }

        public async Task<TEntity> Get<TProperty1, TProperty2>(int id, Expression<Func<TEntity, TProperty1>> navigationPropertyPath, Expression<Func<TEntity, TProperty2>> otherNavigationPropertyPath)
        {
            var result = _context.Set<TEntity>()
                .Include(navigationPropertyPath)
                .Include(otherNavigationPropertyPath)
                .FirstOrDefaultAsync(e => e.Id == id);
            return await result;
        }
    }
    
}