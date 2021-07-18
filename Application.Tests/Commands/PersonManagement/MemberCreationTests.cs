using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PersonManagement.Create;
using Application.Dtos.Request.Create;
using Domain.Validators;
using Domain.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.Commands.PersonManagement
{
    public class MemberCreationTests
    {
        private readonly IServiceProvider _builtServices;

        public MemberCreationTests()
        {
            var services = GetServices();
            _builtServices = TestDependenciesResolver.BuildServices(services);
        }

        private IServiceCollection GetServices() => TestDependenciesResolver.AddServices();

        [Fact]
        public async Task ExecuteAsync_WhenCalledWithValidRequest_CreatesMemberInDb()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var tenantDomainValidator
                = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
            var target = TestDependenciesResolver.GetService<ICreateMemberCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            await TestSeeder.CreateDemoTenant(context, tenantDomainValidator);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();
            var request = new CreateMemberRequestDto
            {
                TenantId = tenant.TenantId,
                Name = "Azeez",
                Surname = "Odumosu",
                DateAndMonthOfBirth = "16/03",
                Gender = "Male",
                PhoneNumber = "000000000000",
                IsWorker = false
            };
            
            // Act
            var response = await target.ExecuteAsync(request);
            
            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.MemberId);
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.IsWorker, response.IsWorker);
        }
    }
}