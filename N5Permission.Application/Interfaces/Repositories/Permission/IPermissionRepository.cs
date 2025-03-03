

using N5Permission.Application.Dtos.Permission;
using N5Permission.Application.Interfaces.Repositories.Base;
using N5Permission.Domain.Entities.Permission;

namespace N5Permission.Application.Interfaces.Repositories.Permission
{
    public interface IPermissionRepository : IBaseRepository<Domain.Entities.Permission.Permission>
    {
        
    }
}
