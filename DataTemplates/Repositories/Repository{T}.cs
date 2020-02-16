namespace DataTemplates.Repositories
{
    using System.Threading.Tasks;
    using DataTemplates.Entities;
    using DataTemplates.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public abstract class Repository<TEntity> : BaseRepository<TEntity>, IRepository<TEntity> where TEntity : Entity, new()
    {
        protected Repository(DbContext context) : base(context)
        {
        }

        public async Task<int> Add(TEntity entity)
        {
            return await InternalAdd(entity)
                .ConfigureAwait(false);
        }

        public async Task Update(TEntity entity)
        {
            await InternalUpdate(entity)
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateModifiedFields(TEntity entity)
        {
            return await InternalUpdateModifiedFields(entity)
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateModifiedFields(TEntity modifiedEntity, TEntity originalEntity)
        {
            return await InternalUpdateModifiedFields(modifiedEntity, originalEntity)
                .ConfigureAwait(false);
        }

        public async Task<bool> FindAndDelete(int id)
        {
            return await InternalFindAndDelete(id)
                .ConfigureAwait(false);
        }

        public async Task Delete(int id)
        {
            await InternalDelete(id)
                .ConfigureAwait(false);
        }
    }
}
