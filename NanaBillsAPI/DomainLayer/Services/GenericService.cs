using DataAccessLayer.Repositories;
using System.Linq.Expressions;

namespace DomainLayer.Services
{
    internal class GenericService<TEntity, TId> : IGenericService<TEntity, TId> where TEntity : class
    {
        private IGenericRepository<TEntity, TId> _genericRepository;

        public GenericService(IGenericRepository<TEntity, TId> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _genericRepository.GetAll();
        }

        public async Task<TEntity> GetById(Expression<Func<TEntity, bool>> predicate)
        {
            return await _genericRepository.GetById(predicate);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            return await _genericRepository.Insert(entity);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            return await _genericRepository.Update(entity);
        }
        public async Task Delete(Expression<Func<TEntity, bool>> predicate)
        {
            await _genericRepository.Delete(predicate);
        }
        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await _genericRepository.Search(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetByUserId(Expression<Func<TEntity, bool>> predicate)
        {
            return await _genericRepository.GetByUserID(predicate);
        }
    }
}
