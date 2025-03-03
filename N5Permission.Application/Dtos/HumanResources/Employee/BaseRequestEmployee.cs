

namespace N5Permission.Application.Dtos.HumanResources.Employee
{
    public record BaseRequestEmployee
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
