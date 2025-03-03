namespace N5Permission.Application.Dtos.HumanResources.Employee
{
    public sealed record CreateEmployeeRequest : BaseRequestEmployee
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
