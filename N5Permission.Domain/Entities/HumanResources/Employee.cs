

using N5Permission.Domain.Common;

namespace N5Permission.Domain.Entities.HumanResources
{
    public partial class Employee : AuditoryEntity
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
