using System.Drawing;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using N5Permission.Application.Dtos.Permission;
using N5Permission.Application.Interfaces.Uow;
using N5Permission.Application.Result;
using N5Permission.Infrastructure.ElasticSearch.Interfaces;
using N5Permission.Infrastructure.Logger;
using N5Permission.Application.Extentions.Permission;
using N5Permission.Infrastructure.Kafka.Interfaces;
using N5Permission.Infrastructure.Kafka.Models;
using N5Permission.Infrastructure.ElasticSearch.Models;
using N5Permission.Infrastructure.Kafka.Enums;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Requests;
namespace N5Permission.Application.Interfaces.Services.Permission
{
    public class PermisionService : IPermisionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IElasticSearchService _elasticSearchService;
        private readonly IKafkaProducerService _kafkaProducerService;
        private readonly ElasticSearchSetting _elasticSearchSetting;

        public PermisionService(IUnitOfWork unitOfWork,
                                ILoggerService loggerService,
                                IElasticSearchService elasticSearchService,
                                IKafkaProducerService kafkaProducerService,
                                IOptions<ElasticSearchSetting> elasticSearchSetting
                                )
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
            _elasticSearchService = elasticSearchService;
            _kafkaProducerService = kafkaProducerService;
            _elasticSearchSetting = elasticSearchSetting.Value;
        }
        public async Task<Response<PermissionDto>> CreateRequestPermission(CreateRequestPermission requestPermission)
        {
            Response<PermissionDto> response = new Response<PermissionDto>();

            try
            {
                _loggerService.LogInformation("RequestPermission operation initiated.");

                var permissionRepository = _unitOfWork.Repository<Domain.Entities.Permission.Permission>();

                response = isPermissionInvalid(requestPermission, response);

                if (response.Succeeded == false)
                    return response;


                if (await permissionRepository.Exists(perm => perm.EmployeeId == requestPermission.EmployeeId.Value
                                                && perm.PermissionTypeId == requestPermission.PermissionTypeId.Value
                                                && perm.IsDeleted == false))
                {
                    response.Message = "This user already has that permission assigned.";
                    response.Succeeded = false;
                    return response;
                }

                Domain.Entities.Permission.Permission permission = requestPermission.ConvertToPermissionEntity();

                await permissionRepository.CreateAsync(permission);

                await _unitOfWork.CommitAsync();

                _loggerService.LogInformation($"RequestPermission operation completed in the DB for Permission ID: {permission.PermissionId}");

                await _elasticSearchService.IndexDocumentAsync<Domain.Entities.Permission.Permission>(_elasticSearchSetting.Index, permission.PermissionId.ToString(), permission);

                _loggerService.LogInformation($"RequestPermission operation completed in Elastic Search for Permission ID: {permission.PermissionId}");

                var operationMessage = new OperationMessage()
                {
                    Id = Guid.NewGuid(),
                    NameOperation = Enum.GetName(typeof(OperationName), OperationName.Request)
                };

                await _kafkaProducerService.ProduceAsync(operationMessage);

                _loggerService.LogInformation($"RequestPermission Message persisted in Kafka with Permission ID: {operationMessage.Id}");

                response.Succeeded = true;
                response.Message = "The permission was given correctly.";

            }
            catch (Exception ex)
            {
                response.Message = "An error occurred trying to create the permission request.";
                response.Succeeded = false;
                _loggerService.LogError(response.Message, ex);
            }

            return response;
        }
        public async Task<Response<PermissionDto>> ModifyRequestPermission(ModifyRequestPermission modifyRequest)
        {
            Response<PermissionDto> response = new Response<PermissionDto>();
            try
            {
                _loggerService.LogInformation($"ModifyPermission operation started for Permission ID: {modifyRequest.PermissionTypeId}.");

                var permissionRepository = _unitOfWork.Repository<Domain.Entities.Permission.Permission>();

                response = isPermissionInvalid(modifyRequest, response);

                if (response.Succeeded == false)
                    return response;

                var permissionToUpdate = await permissionRepository.GetByIdAsync<int>(modifyRequest.PermissionId);

                if (permissionToUpdate == null)
                {
                    response.Message = "The permission you want to modify was not found.";
                    response.Succeeded = false;
                    return response;
                }

                permissionToUpdate.ModifiedBy = modifyRequest.ModifiedBy;
                permissionToUpdate.ModifiedDate = modifyRequest.ModifiedDate;
                permissionToUpdate.EmployeeId = modifyRequest.EmployeeId.Value;
                permissionToUpdate.PermissionId = modifyRequest.PermissionId;
                permissionToUpdate.PermissionTypeId = modifyRequest.PermissionTypeId.Value;
                permissionToUpdate.DateGranted = modifyRequest.DateGranted;

                permissionRepository.Update(permissionToUpdate);
                await _unitOfWork.CommitAsync();

                _loggerService.LogInformation($"ModifyPermission operation completed in the Db for Permission ID: {modifyRequest.PermissionId}");

                await _elasticSearchService.UpdateDocumentAsync<Domain.Entities.Permission.Permission>(_elasticSearchSetting.Index, $"{permissionToUpdate.PermissionId}", permissionToUpdate);

                _loggerService.LogInformation($"ModifyPermission operation completed in Elastic Search for Permission ID:: {modifyRequest.PermissionId}");

                var operationMessage = new OperationMessage()
                {
                    Id = Guid.NewGuid(),
                    NameOperation = Enum.GetName(typeof(OperationName), OperationName.Modify)
                };

                await _kafkaProducerService.ProduceAsync(operationMessage);

                response.Succeeded = true;
                response.Message = "The permission was updated correctly.";


                _loggerService.LogInformation($"ModifyPermission Message persisted in Kafka with Permission ID: {operationMessage.Id}");

            }
            catch (Exception ex)
            {
                response.Message = "An error occurred trying to update the permission request.";
                response.Succeeded = false;
                _loggerService.LogError(response.Message, ex);
            }
            return response;
        }
        public async Task<Response<List<PermissionDto>>> GetPermissions(string permissionId)
        {
            Response<List<PermissionDto>> response = new Response<List<PermissionDto>>();
            try
            {

                if (string.IsNullOrWhiteSpace(permissionId)) 
                {
                    response.Message = "You must provide the permission id to get the permissions.";
                    response.Succeeded = false;
                    return response;
                }

                _loggerService.LogInformation("GetPermissions operation initiated.");

                var permissions = await _elasticSearchService.GetDocumentAsync<List<PermissionDto>>(_elasticSearchSetting.Index, permissionId);
                response.Succeeded = true;
                response.Data = permissions;

                _loggerService.LogInformation("GetPermissions operation completed.");

                var operationMessage = new OperationMessage()
                {
                    Id = Guid.NewGuid(),
                    NameOperation = Enum.GetName(typeof(OperationName), OperationName.Get)
                };

                await _kafkaProducerService.ProduceAsync(operationMessage);

                _loggerService.LogInformation($"GetPermissions Message persisted in Kafka with Permission ID: {operationMessage.Id}");

            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Message = "An error occurred obtaining permissions.";
                _loggerService.LogError(response.Message, ex);
            }
            return response;
        }
        private Response<PermissionDto> isPermissionInvalid(BaseRequestPermission requestPermission, Response<PermissionDto> response)
        {
            if (requestPermission is null)
            {
                response.Message = "The permission object is required to perform this operation.";
                response.Succeeded = false;
                return response;
            }

            if (requestPermission.PermissionTypeId.HasValue == false)
            {
                response.Message = "You must provide the permission type.";
                response.Succeeded = false;
                return response;
            }

            if (requestPermission.PermissionTypeId.Value < 0)
            {
                response.Message = "The permission type is invalid.";
                response.Succeeded = false;
                return response;
            }

            if (requestPermission.EmployeeId.HasValue == false)
            {
                response.Message = "The employee is required to perform this operation.";
                response.Succeeded = false;
                return response;
            }

            if (requestPermission.EmployeeId.Value < 0)
            {
                response.Message = "the employee is invalid.";
                response.Succeeded = false;
                return response;
            }
            return response;
        }

    }
}
