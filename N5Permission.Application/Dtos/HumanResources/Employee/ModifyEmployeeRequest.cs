

namespace N5Permission.Application.Dtos.HumanResources.Employee
{
    public sealed record ModifyEmployeeRequest : BaseRequestEmployee
    {
        public int EmployeeId { get; set; }
        
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
