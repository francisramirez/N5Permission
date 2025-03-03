using N5Permission.Application.Dtos.Base;

namespace N5Permission.Application.Dtos.HumanResources.Employee
{
    public record EmployeeDto : DtoBase<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }


    }
}
