using N5Permission.Application.Dtos.HumanResources.Employee;
using N5Permission.Application.Result;

namespace N5Permission.Application.Interfaces.Services.HumanResources.Employee
{
    public interface IEmployeeService
    {
        Task<Response<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeRequest createRequest);
        Task<Response<EmployeeDto>> ModifyEmployeeAsync(ModifyEmployeeRequest modifyRequest);
        Task<Response<List<EmployeeDto>>> GetAllEmployeesAsync();
    }
}
