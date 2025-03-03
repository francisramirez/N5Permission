
using N5Permission.Application.Dtos.HumanResources.Employee;
using N5Permission.Application.Interfaces.Repositories.HumanResources;
using N5Permission.Application.Interfaces.Uow;
using N5Permission.Application.Result;
using N5Permission.Infrastructure.Logger;
using N5Permission.Application.Extentions.Employee;
using N5Permission.Application.Enums;
using Elastic.Clients.Elasticsearch.MachineLearning;
using Elastic.Clients.Elasticsearch;

namespace N5Permission.Application.Interfaces.Services.HumanResources.Employee
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerSerivce;
        public EmployeeService(IUnitOfWork unitOfWork,
                               ILoggerService loggerService)
        {

            _loggerSerivce = loggerService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeRequest createRequest)
        {
            Response<EmployeeDto> response = new Response<EmployeeDto>();

            try
            {
                _loggerSerivce.LogInformation("CreateEmployee operation initiated.");

                var employeeRepository = _unitOfWork.Repository<Domain.Entities.HumanResources.Employee>();

                var result = isEmployeeInValid(createRequest, response);

                if (!result.Succeeded)
                    return result;

                if (await employeeRepository.Exists(emp => emp.Email == createRequest.Email))
                {
                    response.Message = $"This employee {string.Concat(createRequest.FirstName, " ", createRequest.LastName)} is registered.";
                    response.Succeeded = false;
                    return response;
                }

                var employeeToAdd = createRequest.ConvertToEmployeeEntity();

                await employeeRepository.CreateAsync(employeeToAdd);
                await _unitOfWork.CommitAsync();

                response.Succeeded = true;
                response.Message = "The employee was created successfully.";

                _loggerSerivce.LogInformation($"CreateEmployee operation completed for Employee ID: {employeeToAdd.EmployeeId}");

            }
            catch (Exception ex)
            {
                response.Message = "An error occurred while creating the employee.";
                response.Succeeded = false;
                _loggerSerivce.LogError(response.Message, ex);
            }
            return response;
        }
        public async Task<Response<EmployeeDto>> ModifyEmployeeAsync(ModifyEmployeeRequest modifyRequest)
        {
            Response<EmployeeDto> response = new Response<EmployeeDto>();

            try
            {
                _loggerSerivce.LogInformation("ModifyEmployee operation initiated.");

                var employeeRepository = _unitOfWork.Repository<Domain.Entities.HumanResources.Employee>();

                var result = isEmployeeInValid(modifyRequest, response);

                if (!result.Succeeded)
                    return result;

                var employeeToUpdate = await employeeRepository.GetByIdAsync<int>(modifyRequest.EmployeeId);

                if (employeeToUpdate is null)
                {
                    response.Message = $"This employee {string.Concat(modifyRequest.FirstName, " ", modifyRequest.LastName)} was not found.";
                    response.Succeeded = false;
                    return response;
                }

                employeeToUpdate.FirstName = modifyRequest.FirstName;
                employeeToUpdate.LastName = modifyRequest.LastName;
                employeeToUpdate.Email = modifyRequest.Email;
                employeeToUpdate.ModifiedBy = modifyRequest.ModifiedBy;
                employeeToUpdate.ModifiedDate = modifyRequest.ModifiedDate;

                employeeRepository.Update(employeeToUpdate);
                await _unitOfWork.CommitAsync();

                response.Succeeded = true;
                response.Message = "The employee was updated successfully.";

                _loggerSerivce.LogInformation($"ModifyEmployee operation completed for Employee ID: {employeeToUpdate.EmployeeId}");

            }
            catch (Exception ex)
            {

                response.Message = "An error occurred while updating the employee.";
                response.Succeeded = false;
                _loggerSerivce.LogError(response.Message, ex);
            }
            return response;
        }
        private static Response<EmployeeDto> isEmployeeInValid(BaseRequestEmployee requestEmployee,
                                                               Response<EmployeeDto> response)
        {

            if (requestEmployee is null)
            {
                response.Message = "The object is required to perform this operation.";
                response.Succeeded = false;
                return response;
            }
            if (string.IsNullOrWhiteSpace(requestEmployee.FirstName))
            {
                response.Message = "The field first name is required to perform this operation.";
                response.Succeeded = false;
                return response;
            }
            if (string.IsNullOrWhiteSpace(requestEmployee.LastName))
            {
                response.Message = "The field last name is required to perform this operation.";
                response.Succeeded = false;
                return response;
            }
            if (string.IsNullOrWhiteSpace(requestEmployee.Email))
            {
                response.Message = "The field email is required to perform this operation.";
                response.Succeeded = false;
                return response;
            }


            return response;

        }

        public async Task<Response<List<EmployeeDto>>> GetAllEmployeesAsync()
        {
            Response<List<EmployeeDto>> response = new Response<List<EmployeeDto>>();

            try
            {
                var employeeRepository = _unitOfWork.Repository<Domain.Entities.HumanResources.Employee>();

                response.Data = (from emp in await employeeRepository.GetAllAsync()
                                 where emp.IsDeleted == false
                                 orderby emp.CreatedDate descending
                                 select new EmployeeDto()
                                 {
                                     ChangedBy = emp.CreatedBy,
                                     ChangedDate = emp.CreatedDate,
                                     Email = emp.Email,
                                     FirstName = emp.FirstName,
                                     Id = emp.EmployeeId, 
                                     LastName = emp.LastName,
                                 }).ToList();
            }
            catch (Exception ex)
            {
                response.Message = "An error occurred obtaining employees.";
                response.Succeeded = false;
            }
            return response;
        }
    }
}
