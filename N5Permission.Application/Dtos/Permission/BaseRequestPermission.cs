

namespace N5Permission.Application.Dtos.Permission
{
    public abstract record BaseRequestPermission
    {
        public int? EmployeeId { get; set; }

        public short? PermissionTypeId { get; set; }

        public DateTime DateGranted { get; set; }
    }
}
