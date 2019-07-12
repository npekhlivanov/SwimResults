namespace DataTemplates.Repositories
{
    using DataTemplates.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    public abstract class BaseRepository<TEntity> : ReadOnlyRepository<TEntity>
        where TEntity : Entity, new()
    {
        public BaseRepository(DbContext context) : base(context)
        {
        }

        protected async Task<int> InternalAdd(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbSet = _context.Set<TEntity>();
            var entry = await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entry.Entity.Id;
        }

        protected async Task InternalUpdate(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = _context.Entry<Entity>(entity); // or _context.Attach()?
            entry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        protected async Task<bool> InternalUpdateModifiedFields(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbSet = _context.Set<TEntity>();
            var entityToUpdate = await dbSet.FindAsync(entity.Id);
            if (entityToUpdate == null)
            {
                return false;
            }

            return await InternalUpdateModifiedFields(entityToUpdate, entity);
        }

        protected async Task<bool> InternalUpdateModifiedFields(TEntity modifiedEntity, TEntity originalEntity)
        {
            if (originalEntity == null)
            {
                throw new ArgumentNullException(nameof(originalEntity));
            }

            if (modifiedEntity == null)
            {
                throw new ArgumentNullException(nameof(originalEntity));
            }

            var entry = _context.Entry<Entity>(originalEntity);
            entry.CurrentValues.SetValues(modifiedEntity);
            if (entry.State == EntityState.Unchanged)
            {
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        protected async Task<bool> InternalFindAndDelete(int id)
        {
            var dbSet = _context.Set<TEntity>();
            var entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        protected async Task InternalDelete(int id)
        {
            var entity = new TEntity() { Id = id };
            var entry = _context.Entry(entity);
            entry.State = EntityState.Deleted; // state is Detached and entry is not deleted
            await _context.SaveChangesAsync();
        }
    }
}
