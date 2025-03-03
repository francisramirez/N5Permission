

using N5Permission.Application.Interfaces.Repositories.Base;

namespace N5Permission.Application.Interfaces.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> CommitAsync();

    }
}
