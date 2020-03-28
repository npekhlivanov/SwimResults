namespace NP.DataTemplates.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReadOnlyRepository<TEntity>
        where TEntity : IEntity
    {
        Task<IList<TEntity>> GetList();

        Task<IList<TEntity>> GetList(IQuerySpecification<TEntity> specification);

        Task<TEntity> GetById(int id);

        Task<TEntity> GetById(int id, IIncludeSpecification<TEntity> specification);

        Task<int> GetCount();

        Task<int> GetCount(IQuerySpecification<TEntity> specification); //Expression<Func<TEntity, bool>> predicate);

        //Task<TEntity> GetById(int id, [NotNull] Func<IQueryable<TEntity>, IQueryable<TEntity>> queryModifier);

        //Task<TEntity> GetById<TProperty>(int id, Expression<Func<TEntity, TProperty>> navigationPropertyPath);

        //Task<TEntity> GetById<TProperty1, TProperty2, TProperty3>(int id, Expression<Func<TEntity, TProperty1>> navigationProperty1Path, Expression<Func<TEntity, TProperty2>> navigationProperty2Path, Expression<Func<TEntity, TProperty3>> navigationProperty3Path);

        //Task<TEntity> GetById<TProperty1, TProperty2>(int id, Expression<Func<TEntity, TProperty1>> navigationProperty1Path, Expression<Func<TEntity, TProperty2>> navigationProperty2Path);

        //Task<TEntity> GetById<TProperty1, TProperty2>(int id, Expression<Func<TEntity, TProperty1>> navigationProperty1Path, Expression<Func<TProperty1, TProperty2>> NavigationProperty2Path);

        //Task<IList<TEntity>> GetList(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryModifier);

        //Task<IList<TEntity>> GetList(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryModifier, int pageSize, int pageNo);

        //Task<IList<TEntity>> GetList<TKey>(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryModifier, int pageSize, int pageNo, Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending);

        //Task<IList<TEntity>> GetList<TKey>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending);

        //Task<IList<TEntity>> GetList<TKey>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo);

        //Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate);

        //Task<IList<TEntity>> GetList<TKey, TProperty, TProperty2>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo, Expression<Func<TEntity, TProperty>> navigationPropertyPath, Expression<Func<TEntity, TProperty2>> otherNavigationPropertyPath);

        //Task<IList<TEntity>> GetList<TKey, TProperty, TProperty2>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo, Expression<Func<TEntity, TProperty>> navigationPropertyPath, Expression<Func<TEntity, TProperty2>> otherNavigationPropertyPath);

        //Task<IList<TEntity>> GetList<TKey, TProperty>(Expression<Func<TEntity, TKey>> sortSelector, bool sortDescending, int pageSize, int pageNo, Expression<Func<TEntity, TProperty>> navigationPropertyPath);

        //Task<IList<TEntity>> GetList<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath);
    }
}
