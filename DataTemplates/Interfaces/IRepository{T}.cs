namespace DataTemplates.Interfaces
{
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : IEntity
    {
        Task<int> Add(TEntity entity);

        Task Delete(int id);

        Task<bool> FindAndDelete(int id);

        Task Update(TEntity entity);

        Task<bool> UpdateModifiedFields(TEntity entity);

        Task<bool> UpdateModifiedFields(TEntity modifiedEntity, TEntity originalEntity);
    }
}
