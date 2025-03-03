

using Microsoft.EntityFrameworkCore;
using N5Permission.Application.Interfaces.Repositories.Base;
using N5Permission.Application.Interfaces.Uow;
using N5Permission.Persistence.Context;
using N5Permission.Persistence.Repositories.Base;

namespace N5Permission.Persistence.Uow
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly PermissionContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        public UnitOfWork(PermissionContext context)
        {
            _context = context;
        }
        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new BaseRepository<TEntity>(_context);
                _repositories[type] = repositoryInstance;
            }

            return (IBaseRepository<TEntity>)_repositories[type];
        }
        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();

    }
}
