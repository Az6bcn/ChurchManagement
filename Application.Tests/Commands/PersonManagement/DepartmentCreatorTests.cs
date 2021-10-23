using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PersonManagement.Create;
using Application.Dtos.Request.Create;
using Application.RequestValidators;
using Domain.Entities.PersonAggregate;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.Commands.PersonManagement
{
    public class DepartmentCreatorTests
    {
        private readonly IServiceProvider _builtServices;
        private CreateDepartmentRequestDto _request;

        public DepartmentCreatorTests()
        {
            var services = ResolveServices();
            _builtServices = TestDependenciesResolver.BuildServices(services);
        }

        private IServiceCollection ResolveServices()
            => TestDependenciesResolver.AddServices();

        private async Task<CreateDepartmentRequestDto> GetRequestAsync(ApplicationDbContext context)
        {
            var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);

            await TestSeeder.CreateDemoTenant(context, validator);

            var tenantId = context.Set<Domain.Entities.TenantAggregate.Tenant>()
                                  .AsNoTracking()
                                  .Single()
                                  .TenantId;

            var request = new CreateDepartmentRequestDto
            {
                TenantId = tenantId,
                Name = "Demo Department"
            };

            return request;
        }

        [Fact]
        public async Task ExecuteAsync_WhenCalledWithValidRequest_UpdatesDepartmentInDatabase()
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
        public async Task ExecuteAsync_WhenCalledWithValidRequestAndNonExistentTenantId_ShouldThrowException()
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
        public async Task ExecuteAsync_WhenCalledWithoutName_ShouldThrowException()
        {
            // Arrange
            var context = TestDbCreator.GetApplicationTestDbContext(_builtServices);
            var target = TestDependenciesResolver.GetService<ICreateDepartmentCommand>(_builtServices);

            TestDbCreator.CreateDatabase(context);
            _request = await GetRequestAsync(context);
            _request.Name = string.Empty;

            // Act and Assert
            await Assert.ThrowsAsync<RequestValidationException>(
                                                                 async ()
                                                                     => await target.ExecuteAsync(_request));
        }

        [Fact]
        public async Task ExecuteAsync_WhenCalledWithExistentName_ShouldThrowException()
        {
            // Arrange
            var context = TestDbCreator.GetApplicationTestDbContext(_builtServices);
            var target = TestDependenciesResolver.GetService<ICreateDepartmentCommand>(_builtServices);

            TestDbCreator.CreateDatabase(context);

            _request = await GetRequestAsync(context);

            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();
            await TestSeeder.CreateDemoDepartment(context, tenant);
            var department = context.Set<Department>().Single();

            _request.Name = department.Name;

            // Act and Assert
            await Assert.ThrowsAsync<RequestValidationException>(
                                                                 async ()
                                                                     => await target.ExecuteAsync(_request));
        }
    }
}