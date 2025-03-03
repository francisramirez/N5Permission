

using N5Permission.Domain.Common;

namespace N5Permission.Domain.Entities.Permission
{
    public partial class PermissionType : AuditoryEntity
    {
        public short PermissionTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
