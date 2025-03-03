
using N5Permission.Application.Dtos.HumanResources.Employee;

namespace N5Permission.Application.Extentions.Employee
{
    public static class EmployeeExtention
    {
        public static N5Permission.Domain.Entities.HumanResources.Employee ConvertToEmployeeEntity(this CreateEmployeeRequest createEmployee)
        {
            return new Domain.Entities.HumanResources.Employee()
            {

                Email = createEmployee.Email,
                FirstName = createEmployee.FirstName,
                LastName = createEmployee.LastName
            };
        }
        public static N5Permission.Domain.Entities.HumanResources.Employee ConvertToEmployeeEntity(this ModifyEmployeeRequest modifyEmployee)
        {
            return new Domain.Entities.HumanResources.Employee()
            {

                Email = modifyEmployee.Email,
                FirstName = modifyEmployee.FirstName,
                LastName = modifyEmployee.LastName,
                ModifiedBy = modifyEmployee.ModifiedBy,
                ModifiedDate = DateTime.Now,
                EmployeeId = modifyEmployee.EmployeeId
            };
        }
    }
}
