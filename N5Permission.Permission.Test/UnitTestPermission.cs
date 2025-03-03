using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using FluentAssertions;
using Moq;
using Xunit;
using N5Permission.Application.Interfaces.Services.Permission;
using N5Permission.Application.Interfaces.Uow;
using N5Permission.Infrastructure.ElasticSearch.Interfaces;
using N5Permission.Infrastructure.ElasticSearch.Models;
using N5Permission.Infrastructure.Kafka.Interfaces;
using N5Permission.Infrastructure.Logger;
using N5Permission.Application.Interfaces.Repositories.Base;
using N5Permission.Application.Interfaces.Repositories.Permission;
using N5Permission.Application.Dtos.Permission;

namespace N5Permission.Permission.Test
{
    public class UnitTestPermission
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILoggerService> _mockLoggerService;
        private readonly Mock<IElasticSearchService> _mockElasticSearchService;
        private readonly Mock<IKafkaProducerService> _mockKafkaProducerService;
        private readonly Mock<IPermissionRepository> _mockPermissionRepo;
        private readonly PermisionService _permisionService;
        private readonly ElasticSearchSetting _elasticSearchSetting;

        public UnitTestPermission()
        {
          
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLoggerService = new Mock<ILoggerService>();
            _mockElasticSearchService = new Mock<IElasticSearchService>();
            _mockKafkaProducerService = new Mock<IKafkaProducerService>();
            _mockPermissionRepo = new Mock<IPermissionRepository>();
            _elasticSearchSetting = new ElasticSearchSetting { Index = "permissions" };
            _mockUnitOfWork.Setup(uow => uow.Repository<Domain.Entities.Permission.Permission>())
                           .Returns(_mockPermissionRepo.Object);

            _permisionService = new PermisionService(
                _mockUnitOfWork.Object,
                _mockLoggerService.Object,
                _mockElasticSearchService.Object,
                _mockKafkaProducerService.Object,
                Options.Create(_elasticSearchSetting)
            );
        }

        [Fact]
        public async Task CreateRequestPermission_ShouldReturnSuccess_WhenPermissionIsValid()
        {
            // Arrange
            var requestPermission = new CreateRequestPermission
            {
                EmployeeId = 1,
                PermissionTypeId = 1
            };

            _mockPermissionRepo.Setup(repo => repo.Exists(It.IsAny<Func<Domain.Entities.Permission.Permission, bool>>()))
                               .ReturnsAsync(false);

            _mockPermissionRepo.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Permission.Permission>()))
                               .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);

            // Act

            var response = await _permisionService.CreateRequestPermission(requestPermission);
            
            // Assert
            response.Succeeded.Should().BeTrue();
            response.Message.Should().Be("The permission was given correctly.");
            _mockLoggerService.Verify(log => log.LogInformation(It.IsAny<string>()), Times.Exactly(3));

        }

        [Fact]
        public async Task CreateRequestPermission_ShouldReturnFailure_WhenPermissionAlreadyExists()
        {
            // Arrange
            var requestPermission = new CreateRequestPermission
            {
                EmployeeId = 1,
                PermissionTypeId = 1
            };
            _mockPermissionRepo.Setup(repo => repo.Exists(It.IsAny<Func<Domain.Entities.Permission.Permission, bool>>()))
                               .ReturnsAsync(true);
            // Act
            var response = await _permisionService.CreateRequestPermission(requestPermission);
            // Assert
            response.Succeeded.Should().BeFalse();
            response.Message.Should().Be("This user already has that permission assigned.");
        }

        [Fact]
        public async Task ModifyRequestPermission_ShouldReturnSuccess_WhenPermissionIsFound()
        {
            // Arrange
            var modifyRequest = new ModifyRequestPermission
            {
                PermissionId = 1,
                EmployeeId = 1,
                PermissionTypeId = 1
            };
            _mockPermissionRepo.Setup(repo => repo.GetByIdAsync<int>(modifyRequest.PermissionId))
                               .ReturnsAsync((Domain.Entities.Permission.Permission)null);
            // Act
            var response = await _permisionService.ModifyRequestPermission(modifyRequest);
            // Assert
            response.Succeeded.Should().BeFalse();
            response.Message.Should().Be("The permission you want to modify was not found.");
        }
         

    }
}