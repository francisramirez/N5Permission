using System.Linq.Expressions;

namespace N5Permission.Application.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task CreateAsync(params TEntity[] entities);
        void Update(TEntity entity);
        void Update(params TEntity[] entities);
        void Remove(TEntity entity);
        void Remove(params TEntity[] entities);
        Task<TEntity> GetByIdAsync<TType>(TType id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);
    }
}
