
using N5Permission.Application.Dtos.Permission;

namespace N5Permission.Application.Extentions.Permission
{
    public static class PermissionExtention
    {
        public static Domain.Entities.Permission.Permission ConvertToPermissionEntity(this CreateRequestPermission createRequest)
        {
            return new Domain.Entities.Permission.Permission()
            {
                DateGranted = createRequest.DateGranted,
                EmployeeId = createRequest.EmployeeId.Value,
                PermissionTypeId = createRequest.PermissionTypeId.Value,
                CreatedDate = createRequest.CreatedDate,
                CreatedBy = createRequest.CreatedBy
            };
        }
       
    }
}
