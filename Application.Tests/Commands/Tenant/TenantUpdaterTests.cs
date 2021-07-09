using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Tenant.Update;
using Application.Dtos.Request.Update;
using Application.RequestValidators;
using Domain.Validators;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;
using Xunit;

namespace Application.Tests.Commands.Tenant
{
    public class TenantUpdaterTests
    {
        private ApplicationTestDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public TenantUpdaterTests()
        {
            var services = ResolveServices();
            _serviceProvider = services.BuildServiceProvider();
        }

        private IServiceCollection ResolveServices() => TestDependenciesResolver.AddServices();

        [Fact]
        public async Task Update_WhenCalledWithValidRequest_ShouldUpdateTenantInDatabase()
        {
            // Arrange
            var context = TestDbCreator.GetApplicationTestDbContext(_serviceProvider);
            var target = TestDependenciesResolver.GetService<IUpdateTenantCommand>(_serviceProvider);
            var tenantCreationValidator = TestDependenciesResolver.GetService<IValidateTenantInDomain>
                (_serviceProvider);
            TestDbCreator.CreateDatabase(context);
            await TestSeeder.CreateDemoTenant(context, tenantCreationValidator);

            // Act
            var createdTenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().Single();
            var updateRequest = new UpdateTenantRequestDto
            {
                TenantId = createdTenant.TenantId,
                Name = "Updated Demo",
                CurrencyId = CurrencyEnum.Naira,
                TenantStatus = TenantStatusEnum.Cancelled,
                LogoUrl = "www.https://nothing.com"
            };
            var updatedTenantResponseDto = await target.ExecuteAsync(updateRequest);

            var insertedUpdate = context.Set<Domain.Entities.TenantAggregate.Tenant>().Single();
            // Assert
            Assert.NotNull(updatedTenantResponseDto);
            Assert.Equal(updateRequest.TenantId, updatedTenantResponseDto.TenantId);
            Assert.Equal(updateRequest.Name, updatedTenantResponseDto.Name);
            Assert.Equal(updateRequest.CurrencyId, updatedTenantResponseDto.Currency);
            Assert.Equal(updateRequest.LogoUrl, updatedTenantResponseDto.LogoUrl);

            Assert.Equal(updateRequest.TenantId, insertedUpdate.TenantId);
            Assert.Equal(updateRequest.Name, insertedUpdate.Name);
            Assert.Equal((int)updateRequest.CurrencyId, insertedUpdate.CurrencyId);
            Assert.Equal(updateRequest.LogoUrl, insertedUpdate.LogoUrl);
        }

        [Fact]
        public async Task Update_WhenCalledWithOutName_ShouldThrowException()
        {
            // Arrange
            var context = TestDbCreator.GetApplicationTestDbContext(_serviceProvider);
            var target = TestDependenciesResolver.GetService<IUpdateTenantCommand>(_serviceProvider);
            var tenantCreationValidator = TestDependenciesResolver.GetService<IValidateTenantInDomain>
                (_serviceProvider);
            TestDbCreator.CreateDatabase(context);
            await TestSeeder.CreateDemoTenant(context, tenantCreationValidator);

            // Act
            var createdTenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().Single();
            var updateRequest = new UpdateTenantRequestDto
            {
                TenantId = createdTenant.TenantId,
                Name = "",
                CurrencyId = CurrencyEnum.Naira,
                LogoUrl = "www.https://nothing.com"
            };

            await Assert.ThrowsAsync<RequestValidationException>(async ()
                         => await target.ExecuteAsync(updateRequest));
        }

        [Fact]
        public async Task Update_WhenCalledWithOutCurrency_ShouldThrowException()
        {
            // Arrange
            var context = TestDbCreator.GetApplicationTestDbContext(_serviceProvider);
            var target = TestDependenciesResolver.GetService<IUpdateTenantCommand>(_serviceProvider);
            var tenantCreationValidator = TestDependenciesResolver.GetService<IValidateTenantInDomain>
                (_serviceProvider);
            TestDbCreator.CreateDatabase(context);
            await TestSeeder.CreateDemoTenant(context, tenantCreationValidator);

            // Act
            var createdTenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().Single();
            var updateRequest = new UpdateTenantRequestDto
            {
                TenantId = createdTenant.TenantId,
                Name = "Update dEMO",
                CurrencyId = 0,
                LogoUrl = "www.https://nothing.com"
            };

            await Assert.ThrowsAsync<DomainValidationException>(async ()
                    => await target.ExecuteAsync(updateRequest));
        }

        [Fact]
        public async Task Update_WhenCalledWithOutValidTenantStatus_ShouldThrowException()
        {
            // Arrange
            var context = TestDbCreator.GetApplicationTestDbContext(_serviceProvider);
            var target = TestDependenciesResolver.GetService<IUpdateTenantCommand>(_serviceProvider);
            var tenantCreationValidator = TestDependenciesResolver.GetService<IValidateTenantInDomain>
                (_serviceProvider);
            TestDbCreator.CreateDatabase(context);
            await TestSeeder.CreateDemoTenant(context, tenantCreationValidator);

            // Act
            var createdTenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().Single();
            var updateRequest = new UpdateTenantRequestDto
            {
                TenantId = createdTenant.TenantId,
                Name = "Update dEMO",
                CurrencyId = CurrencyEnum.Naira,
                TenantStatus = 0,
                LogoUrl = "www.https://nothing.com"
            };

            await Assert.ThrowsAsync<DomainValidationException>(async ()
                                                                    => await target.ExecuteAsync(updateRequest));
        }

    }
}