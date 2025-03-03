using MediatR;
using N5Permission.Application.Dtos.HumanResources.Employee;
using N5Permission.Application.Interfaces.Services.HumanResources.Employee;
using N5Permission.Application.Result;

namespace N5Permission.Application.Features.HumanResources.Commands
{
    public sealed record CreateEmployeeCommand(CreateEmployeeRequest createEmployee) : IRequest<Response<EmployeeDto>>;
    
    public sealed class CreateTaskCommandHandler : IRequestHandler<CreateEmployeeCommand, Response<EmployeeDto>>
    {
        private readonly IEmployeeService _employeeService;
        public CreateTaskCommandHandler(IEmployeeService employeeService) => _employeeService = employeeService;
        public async Task<Response<EmployeeDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken) => await _employeeService.CreateEmployeeAsync(request.createEmployee);
    }

}
