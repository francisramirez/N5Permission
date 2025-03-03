

using N5Permission.Application.Dtos.Permission;
using N5Permission.Application.Interfaces.Repositories.Permission;
using N5Permission.Persistence.Context;
using N5Permission.Persistence.Repositories.Base;

namespace N5Permission.Persistence.Repositories.Permission
{
    public sealed class PermissionRepository : BaseRepository<Domain.Entities.Permission.Permission>, IPermissionRepository
    {
        public PermissionRepository(PermissionContext context) : base(context)
        {
            
        }

        
    }
}
