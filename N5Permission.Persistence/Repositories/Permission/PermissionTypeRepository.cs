

using N5Permission.Application.Interfaces.Repositories.Permission;
using N5Permission.Domain.Entities.Permission;
using N5Permission.Persistence.Context;
using N5Permission.Persistence.Repositories.Base;

namespace N5Permission.Persistence.Repositories.Permission
{
    public sealed class PermissionTypeRepository :BaseRepository<PermissionType>, IPermissionTypeRepository
    {
        public PermissionTypeRepository(PermissionContext context) : base(context) 
        {
            
        }
    }
    
}
