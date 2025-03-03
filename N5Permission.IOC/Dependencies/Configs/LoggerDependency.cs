using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using N5Permission.Infrastructure.Logger;
using Serilog;


namespace N5Permission.IOC.Dependencies.Configs
{
    public static class LoggerDependency
    {
        public static void AddLoggerDependency(this WebApplicationBuilder builder) 
        {
            // Configure Serilog
            builder.Host.UseSerilog((context, config) =>
            {
                config
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File("logs/api_log.txt", rollingInterval: RollingInterval.Day);
            });

            builder.Services.AddSingleton<ILoggerService, LoggerService>();

        }
    }
}
