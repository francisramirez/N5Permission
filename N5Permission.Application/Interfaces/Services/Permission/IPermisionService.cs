

using N5Permission.Application.Dtos.Permission;
using N5Permission.Application.Result;
using N5Permission.Domain.Entities.Permission;

namespace N5Permission.Application.Interfaces.Services.Permission
{
    public interface IPermisionService
    {
        Task<Response<PermissionDto>> CreateRequestPermission(CreateRequestPermission requestPermission);
        Task<Response<PermissionDto>> ModifyRequestPermission(ModifyRequestPermission modifyRequest);
        Task<Response<List<PermissionDto>>> GetPermissions(string permissionId);
    }
}
