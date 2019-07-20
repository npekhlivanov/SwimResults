namespace DataTemplates.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IReadOnyReposotory<TEntity> 
        where TEntity : IEntity
    {
        Task<IList<TEntity>> GetList();
        Task<IList<TEntity>> GetList<TKey>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending);
        Task<IList<TEntity>> GetList<TKey>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo);
        Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate);
        Task<int> GetCount();
        Task<TEntity> Get(int id);
    }
}
