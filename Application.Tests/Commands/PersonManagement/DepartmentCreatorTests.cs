using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PersonManagement.Create;
using Application.Commands.Tenant.Create;
using Application.Dtos.Request.Create;
using Application.RequestValidators;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.Commands.PersonManagement
{
    public class DepartmentCreatorTests
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _builtServices;
        private CreateDepartmentRequestDto _request;

        public DepartmentCreatorTests()
        {
            _services = GetServices();
            _builtServices = TestDependenciesResolver.BuildServices(_services);
        }

        private IServiceCollection GetServices() 
            => TestDependenciesResolver.AddServices();

        private async Task<CreateDepartmentRequestDto> GetRequestAsync(ApplicationDbContext context)
        {
            //var target = TestDependenciesResolver.GetService<ICreateTenantCommand>(_builtServices);
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
            //var validator = TestDependenciesResolver.GetService<IValidatePersonManagementRequestDto>(_builtServices);
            var target = TestDependenciesResolver.GetService<ICreateDepartmentCommand>(_builtServices);
            
            TestDbCreator.CreateDatabase(context);
            _request = await GetRequestAsync(context);
            //await TestDbCreator.SaveChangesAsync(context);
            
            // Act
            var response = await target.ExecuteAsync(_request);

            // Assert
            Assert.NotNull(response);
        } 
    }
}