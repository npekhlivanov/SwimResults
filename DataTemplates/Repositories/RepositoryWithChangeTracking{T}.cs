namespace NP.DataTemplates.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NP.DataTemplates.Entities;
    using NP.DataTemplates.Interfaces;
    using NP.Helpers;

    public abstract class RepositoryWithChangeTracking<TEntityWithChangeTracking> : BaseRepository<TEntityWithChangeTracking>
        where TEntityWithChangeTracking : EntityWithChangeTracking, new()
    {
        private readonly ICurrentUserService currentUserService;

        protected RepositoryWithChangeTracking(DbContext dbContext, ICurrentUserService currentUserService) : base(dbContext)
        {
            this.currentUserService = currentUserService;
        }

        public async Task<int> Add(TEntityWithChangeTracking entity)
        {
            entity.ValidateNotNull(nameof(entity));

            entity.CreatedOn = DateTime.Now;
            var userId = currentUserService?.UserId;
            if (!string.IsNullOrEmpty(userId))
            {
                entity.CreatedBy = userId;
            }

            return await InternalAdd(entity)
                .ConfigureAwait(false);
        }

        public async void Update(TEntityWithChangeTracking entity)
        {
            entity.ValidateNotNull(nameof(entity));

            UpdateEntity(entity);
            await InternalUpdate(entity)
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateModifiedFields(TEntityWithChangeTracking entity)
        {
            entity.ValidateNotNull(nameof(entity));

            UpdateEntity(entity);
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

        private void UpdateEntity(TEntityWithChangeTracking entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var userId = currentUserService?.UserId;
            if (!string.IsNullOrEmpty(userId))
            {
                entity.ModifiedBy = userId;
            }
        }
    }
}
