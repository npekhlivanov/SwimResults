namespace DataTemplates.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IReadOnlyRepository<TEntity>
        where TEntity : IEntity
    {
        Task<TEntity> Get(int id);

        Task<TEntity> Get<TProperty>(int id, Expression<Func<TEntity, TProperty>> navigationPropertyPath);

        Task<TEntity> Get<TProperty1, TProperty2, TProperty3>(int id, Expression<Func<TEntity, TProperty1>> navigationProperty1Path, Expression<Func<TEntity, TProperty2>> navigationProperty2Path, Expression<Func<TEntity, TProperty3>> navigationProperty3Path);

        Task<TEntity> Get<TProperty1, TProperty2>(int id, Expression<Func<TEntity, TProperty1>> navigationProperty1Path, Expression<Func<TEntity, TProperty2>> navigationProperty2Path);

        Task<TEntity> Get<TProperty1, TProperty2>(int id, Expression<Func<TEntity, TProperty1>> navigationProperty1Path, Expression<Func<TProperty1, TProperty2>> NavigationProperty2Path);

        Task<int> GetCount();

        Task<int> GetCount(Expression<Func<TEntity, bool>> predicate);

        Task<IList<TEntity>> GetList();

        Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate);

        Task<IList<TEntity>> GetList<TKey, TProperty, TProperty2>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo, Expression<Func<TEntity, TProperty>> navigationPropertyPath, Expression<Func<TEntity, TProperty2>> otherNavigationPropertyPath);

        Task<IList<TEntity>> GetList<TKey, TProperty, TProperty2>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo, Expression<Func<TEntity, TProperty>> navigationPropertyPath, Expression<Func<TEntity, TProperty2>> otherNavigationPropertyPath);

        Task<IList<TEntity>> GetList<TKey, TProperty>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo, Expression<Func<TEntity, TProperty>> navigationPropertyPath);

        Task<IList<TEntity>> GetList<TKey>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending);

        Task<IList<TEntity>> GetList<TKey>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo);

        Task<IList<TEntity>> GetList<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath);
    }
}
