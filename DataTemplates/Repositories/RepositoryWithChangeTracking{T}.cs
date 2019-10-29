namespace DataTemplates.Repositories
{
    using System;
    using System.Threading.Tasks;
    using DataTemplates.Entities;
    using Microsoft.EntityFrameworkCore;

    public abstract class RepositoryWithChangeTracking<TEntityWithChangeTracking> : BaseRepository<TEntityWithChangeTracking>
        where TEntityWithChangeTracking : EntityWithChangeTracking, new()
    {

        public RepositoryWithChangeTracking(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> Add(TEntityWithChangeTracking entity, string userName)
        {
            entity.CreatedOn = DateTime.Now;
            if (!string.IsNullOrEmpty(userName))
            {
                entity.CreatedBy = userName;
            }

            return await InternalAdd(entity);
        }

        public async void Update(TEntityWithChangeTracking entity, string userName)
        {
            UpdateEntity(entity, userName);
            await InternalUpdate(entity);
        }

        public async Task<bool> UpdateModifiedFields(TEntityWithChangeTracking entity, string userName)
        {
            UpdateEntity(entity, userName);
            return await InternalUpdateModifiedFields(entity);
        }

        public async Task<bool> FindAndDelete(int id)
        {
            return await InternalFindAndDelete(id);
        }

        public async Task Delete(int id)
        {
            await InternalDelete(id);
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
