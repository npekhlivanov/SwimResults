namespace DataTemplates.Interfaces
{
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IReadOnyReposotory<TEntity> 
        where TEntity: IEntity
    {
        Task<int> Add(TEntity entity);
        Task Update(TEntity entity);
        Task<bool> UpdateModifiedFields(TEntity entity);
        Task<bool> FindAndDelete(int id);
        Task Delete(int id);
    }
}
