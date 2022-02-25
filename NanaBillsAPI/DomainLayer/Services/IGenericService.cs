using System.Linq.Expressions;


namespace DomainLayer.Services
{
    public interface IGenericService<TEntity, TId> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetByUserId(Expression<Func<TEntity, bool>> predicate);
    }
}
