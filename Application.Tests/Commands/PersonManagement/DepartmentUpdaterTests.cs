using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PersonManagement.Create;
using Application.Commands.Tenant.Create;
using Application.Dtos.Request.Create;
using Application.RequestValidators;
using Domain.Entities.PersonAggregate;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.Commands.PersonManagement
{
    public class DepartmentUpdaterTests
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _builtServices;
        private CreateDepartmentRequestDto _request;

        public DepartmentUpdaterTests()
        {
            _services = GetServices();
            _builtServices = TestDependenciesResolver.BuildServices(_services);
        }

        private IServiceCollection GetServices()
            => TestDependenciesResolver.AddServices();

        private async Task<CreateDepartmentRequestDto> GetRequestAsync(ApplicationDbContext context)
        {
            var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);

            await TestSeeder.CreateDemoTenant(context, validator);

            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().Single();

            var request = new CreateDepartmentRequestDto
            {
                TenantId = tenant.TenantId,
                Name = "Demo Department"
            };

            return request;
        }

        [Fact]
        public async Task Create_WhenCalledWithValidRequest_AddsDepartmentToDatabase()
        {
            // Arrange
            var context = TestDbCreator.GetApplicationTestDbContext(_builtServices);
            var target = TestDependenciesResolver.GetService<ICreateDepartmentCommand>(_builtServices);

            TestDbCreator.CreateDatabase(context);
            _request = await GetRequestAsync(context);

            // Act
            var response = await target.ExecuteAsync(_request);

            // Assert
            Assert.NotNull(response);
        }


        [Fact]
        public async Task Create_WhenCalledWithValidRequestAndNonExistentTenantId_ShouldThrowException()
        {
            // Arrange
            var context = TestDbCreator.GetApplicationTestDbContext(_builtServices);
            var target = TestDependenciesResolver.GetService<ICreateDepartmentCommand>(_builtServices);

            TestDbCreator.CreateDatabase(context);
            _request = new CreateDepartmentRequestDto
            {
                TenantId = 10000000,
                Name = "Demo Department"
            };

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(
                                                        async () => await target.ExecuteAsync(_request));
        }

        [Fact]
        public async Task Create_WhenCalledWithoutName_ShouldThrowException()
        {
            // Arrange
            var context = TestDbCreator.GetApplicationTestDbContext(_builtServices);
            var target = TestDependenciesResolver.GetService<ICreateDepartmentCommand>(_builtServices);

            TestDbCreator.CreateDatabase(context);
            _request = await GetRequestAsync(context);
            _request.Name = string.Empty;

            // Act and Assert
            await Assert.ThrowsAsync<RequestValidationException>(
                    async () => await target.ExecuteAsync(_request));
        }

        [Fact]
        public async Task Create_WhenCalledWithExsistingName_ShouldThrowException()
        {
            // Arrange
            var context = TestDbCreator.GetApplicationTestDbContext(_builtServices);
            var target = TestDependenciesResolver.GetService<ICreateDepartmentCommand>(_builtServices);

            TestDbCreator.CreateDatabase(context);

            _request = await GetRequestAsync(context);
            
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().Single();
            await TestSeeder.CreateDemoDepartment(context, tenant);
            var department = context.Set<Department>().Single();

            _request.Name = department.Name;
            
            // Act and Assert
            await Assert.ThrowsAsync<RequestValidationException>(
                 async () => await target.ExecuteAsync(_request));
        }
    }
}