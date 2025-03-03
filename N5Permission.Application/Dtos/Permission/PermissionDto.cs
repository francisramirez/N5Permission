


using N5Permission.Application.Dtos.Base;

namespace N5Permission.Application.Dtos.Permission
{
    public record PermissionDto : DtoBase<int>
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateGranted { get; set; }
        public int PermissionTypeId { get; set; }
        public string PermissionTypeName { get; set; }
       
    }
}
