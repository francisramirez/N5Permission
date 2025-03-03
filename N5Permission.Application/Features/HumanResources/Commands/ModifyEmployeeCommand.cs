

using MediatR;
using N5Permission.Application.Dtos.HumanResources.Employee;
using N5Permission.Application.Features.HumanResources.Commands;
using N5Permission.Application.Interfaces.Services.HumanResources.Employee;
using N5Permission.Application.Result;

namespace N5Permission.Application.Features.Employee.Commands
{
    public record class ModifyEmployeeCommand(ModifyEmployeeRequest modifyEmployee) : IRequest<Response<EmployeeDto>>;
    public sealed class ModifyEmployeeCommandHandler : IRequestHandler<ModifyEmployeeCommand, Response<EmployeeDto>>
    {
        private readonly IEmployeeService _employeeService;
        public ModifyEmployeeCommandHandler(IEmployeeService employeeService) => _employeeService = employeeService;
        public async Task<Response<EmployeeDto>> Handle(ModifyEmployeeCommand request, CancellationToken cancellationToken) => await _employeeService.ModifyEmployeeAsync(request.modifyEmployee);
    }
}
