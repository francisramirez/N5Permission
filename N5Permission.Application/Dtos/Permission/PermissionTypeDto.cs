

using N5Permission.Application.Dtos.Base;

namespace N5Permission.Application.Dtos.Permission
{
    public record  PermissionTypeDto : DtoBase<short>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}
