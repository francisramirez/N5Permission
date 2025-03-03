
using Confluent.Kafka;
using Elastic.Clients.Elasticsearch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using N5Permission.Infrastructure.ElasticSearch.Interfaces;
using N5Permission.Infrastructure.ElasticSearch.Models;
using N5Permission.Infrastructure.ElasticSearch.Services;
using N5Permission.Infrastructure.Kafka.Interfaces;
using N5Permission.Infrastructure.Kafka.Models;
using N5Permission.Infrastructure.Kafka.Services;
using N5Permission.Infrastructure.Logger;
using N5Permission.IOC.Dependencies.Configs;
using N5Permission.IOC.Dependencies.Employees;
using N5Permission.IOC.Dependencies.Permissions;
using N5Permission.Persistence.Context;
using Serilog;

namespace N5Permission.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<PermissionContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PermissionDb")));

            #region "Permission App Dependencies"
            //Logger Services Dependencies //
            builder.AddLoggerDependency();

            //Elastic Search Services Dependencies //
            builder.AddElasticSearchDependency();

            //Kafka Services Dependencies //
            builder.AddKafkaDependency();

            //Permission Module Dependencies //
            builder.AddPermissionDependency();

            // Employee Module Dependncy //
            builder.AddEmployeeDependency();
            #endregion


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
