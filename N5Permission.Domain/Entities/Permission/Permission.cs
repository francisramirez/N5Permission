using N5Permission.Domain.Common;
using N5Permission.Domain.Entities.HumanResources;


namespace N5Permission.Domain.Entities.Permission
{
    public partial class Permission : AuditoryEntity
    {
        public int PermissionId { get; set; }

        public int EmployeeId { get; set; }

        public short PermissionTypeId { get; set; }

        public DateTime DateGranted { get; set; }

        public  Employee Employee { get; set; }

        public PermissionType PermissionType { get; set; }
    }
}
