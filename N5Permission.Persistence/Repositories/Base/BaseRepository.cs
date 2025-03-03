

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using N5Permission.Application.Interfaces.Repositories.Base;
using N5Permission.Persistence.Context;
using System.Linq.Expressions;

namespace N5Permission.Persistence.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly PermissionContext _context;

        private DbSet<TEntity> _entities;
        public BaseRepository(PermissionContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }
        public virtual async Task CreateAsync(params TEntity[] entities) => await _entities.AddRangeAsync(entities);

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter) => await _entities.AnyAsync(filter);

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _entities.ToListAsync();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter) => await _entities.Where(filter).ToListAsync();

        public virtual async Task<TEntity> GetByIdAsync<TType>(TType id) => await _entities.FindAsync(id);

        public void Remove(TEntity entity) => _entities.Remove(entity);

        public void Remove(params TEntity[] entities) => _entities.RemoveRange(entities);

        public void Update(TEntity entity) => _entities.Update(entity);

        public void Update(params TEntity[] entities) => _entities.UpdateRange(entities);
    }
}
