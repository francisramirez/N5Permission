using MediatR;
using N5Permission.Application.Dtos.HumanResources.Employee;
using N5Permission.Application.Features.Permission.Queries;
using N5Permission.Application.Interfaces.Services.HumanResources.Employee;
using N5Permission.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Permission.Application.Features.Employee.Queries
{
    public sealed class GetAllEmployeeQuery : IRequest<Response<List<EmployeeDto>>>;

    public class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeQuery, Response<List<EmployeeDto>>>
    {
        private readonly IEmployeeService _employeeService;
        public GetAllEmployeeHandler(IEmployeeService employeeService) => _employeeService = employeeService;
        public async Task<Response<List<EmployeeDto>>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken) => await _employeeService.GetAllEmployeesAsync();
    }



}
