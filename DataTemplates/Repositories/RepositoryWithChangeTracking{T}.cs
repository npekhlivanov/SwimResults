namespace DataTemplates.Repositories
{
    using System;
    using System.Threading.Tasks;
    using DataTemplates.Entities;
    using Microsoft.EntityFrameworkCore;

    public abstract class RepositoryWithChangeTracking<TEntityWithChangeTracking> : BaseRepository<TEntityWithChangeTracking>
        where TEntityWithChangeTracking : EntityWithChangeTracking, new()
    {

        protected RepositoryWithChangeTracking(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> Add(TEntityWithChangeTracking entity, string userName)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.CreatedOn = DateTime.Now;
            if (!string.IsNullOrEmpty(userName))
            {
                entity.CreatedBy = userName;
            }

            return await InternalAdd(entity)
                .ConfigureAwait(false);
        }

        public async void Update(TEntityWithChangeTracking entity, string userName)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            UpdateEntity(entity, userName);
            await InternalUpdate(entity)
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateModifiedFields(TEntityWithChangeTracking entity, string userName)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            UpdateEntity(entity, userName);
            return await InternalUpdateModifiedFields(entity)
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

        private void UpdateEntity(TEntityWithChangeTracking entity, string userName)
        {
            entity.ModifiedOn = DateTime.Now;
            if (!string.IsNullOrEmpty(userName))
            {
                entity.ModifiedBy = userName;
            }
        }
    }
}
