

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using N5Permission.Domain.Entities.HumanResources;
using N5Permission.Domain.Entities.Permission;
using N5Permission.Persistence.EntitiesConfiguration;

namespace N5Permission.Persistence.Context
{
    public partial class PermissionContext : DbContext
    {
        public PermissionContext(DbContextOptions<PermissionContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionTypeConfiguration());
        }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }

       
    }
}
