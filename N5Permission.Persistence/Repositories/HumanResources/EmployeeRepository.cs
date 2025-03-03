

using N5Permission.Application.Interfaces.Repositories.HumanResources;
using N5Permission.Domain.Entities.HumanResources;
using N5Permission.Persistence.Context;

namespace N5Permission.Persistence.Repositories.HumanResources
{
    public sealed class EmployeeRepository : Base.BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(PermissionContext context) : base(context)
        {
            
        }
        
    }
}
