

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using N5Permission.Application.Interfaces.Repositories.HumanResources;
using N5Permission.Application.Interfaces.Services.HumanResources.Employee;
using N5Permission.Persistence.Repositories.HumanResources;
using System.Runtime.CompilerServices;

namespace N5Permission.IOC.Dependencies.Employees
{
    public static class EmployeeDependency
    {
        public static void AddEmployeeDependency(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();
        }
    }
}
