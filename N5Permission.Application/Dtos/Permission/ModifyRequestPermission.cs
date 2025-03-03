

namespace N5Permission.Application.Dtos.Permission
{
    public record ModifyRequestPermission : BaseRequestPermission
    {
        public int PermissionId { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
