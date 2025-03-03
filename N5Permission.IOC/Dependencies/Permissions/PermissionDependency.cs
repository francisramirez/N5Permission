using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using N5Permission.Application.Interfaces.Repositories.Permission;
using N5Permission.Application.Interfaces.Services.Permission;
using N5Permission.Persistence.Repositories.Permission;

namespace N5Permission.IOC.Dependencies.Permissions
{
    public static class PermissionDependency
    {
        public static void AddPermissionDependency(this WebApplicationBuilder builder) 
        {
            builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
            builder.Services.AddTransient<IPermisionService, PermisionService>();
        }
    }
}
