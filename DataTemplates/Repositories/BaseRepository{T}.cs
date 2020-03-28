namespace NP.DataTemplates.Repositories
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NP.DataTemplates.Entities;
    using NP.Helpers;

    public abstract class BaseRepository<TEntity> : ReadOnlyRepository<TEntity>
        where TEntity : Entity, new()
    {
        protected BaseRepository(DbContext context) : base(context)
        {
        }

        protected async Task<int> InternalAdd(TEntity entity)
        {
            entity.ValidateNotNull(nameof(entity));

            var entry = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync()
                .ConfigureAwait(false);
            return entry.Entity.Id;
        }

        protected async Task InternalUpdate(TEntity entity)
        {
            entity.ValidateNotNull(nameof(entity));

            var entry = _context.Entry<Entity>(entity); // or _context.Attach()?
            entry.State = EntityState.Modified;
            await _context.SaveChangesAsync()
                .ConfigureAwait(false);
        }

        protected async Task<bool> InternalUpdateModifiedFields(TEntity entity)
        {
            entity.ValidateNotNull(nameof(entity));

            var entityToUpdate = await _dbSet.FindAsync(entity.Id);
            if (entityToUpdate == null)
            {
                return false;
            }

            return await InternalUpdateModifiedFields(entityToUpdate, entity)
                .ConfigureAwait(false);
        }

        protected async Task<bool> InternalUpdateModifiedFields(TEntity modifiedEntity, TEntity originalEntity)
        {
            originalEntity.ValidateNotNull(nameof(originalEntity));
            modifiedEntity.ValidateNotNull(nameof(modifiedEntity));

            var entry = _context.Entry<Entity>(originalEntity);
            entry.CurrentValues.SetValues(modifiedEntity);
            if (entry.State == EntityState.Unchanged)
            {
                return false;
            }

            await _context.SaveChangesAsync()
                .ConfigureAwait(false);
            return true;
        }

        protected async Task<bool> InternalFindAndDelete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync()
                .ConfigureAwait(false);
            return true;
        }

        protected async Task InternalDelete(int id)
        {
            var entity = new TEntity() { Id = id };
            var entry = _context.Entry(entity);
            entry.State = EntityState.Deleted; // state is Detached and entry is not deleted
            await _context.SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }
}
