
namespace N5Permission.Application.Dtos.Permission
{
    public record CreateRequestPermission : BaseRequestPermission
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
