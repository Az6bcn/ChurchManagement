using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Tenant.Create;
using Application.Commands.Tenant.Update;
using Application.Dtos.Request.Create;
using Application.Dtos.Request.Update;
using Application.RequestValidators;
using Domain.Entities.TenantAggregate;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;
using Xunit;

namespace Application.Tests.Commands.Tenant
{
    public class TenantTests
    {
        private ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        
        public TenantTests()
        {
            var services = ResolveServices();
            _serviceProvider = TestDependenciesResolver.BuildServices(services);
        }

        private IServiceCollection ResolveServices()
        {
            return TestDependenciesResolver.AddServices();
        }

        [Fact]
        public async Task Create_WhenCalledWithValidRequest_ShouldCreateTenantInDatabase()
        {
            // Arrange
            _context = TestDbCreator.GetApplicationTestDbContext(_serviceProvider);
            TestDbCreator.CreateDatabase(_context);
            var target = TestDependenciesResolver.GetService<ICreateTenantCommand>(_serviceProvider);
            
            var tenantRequestDto = new CreateTenantRequestDto()
            {
                Name = "Demo",
                LogoUrl = string.Empty,
                CurrencyId = CurrencyEnum.UsDollars
            };

            // Act
            var response = await target.ExecuteAsync(tenantRequestDto);
            var insertedEntities = _context.ChangeTracker.Entries().ToList();
            
            // Assert
            Assert.NotNull(response);
            Assert.True(response.TenantId > 0);
            Assert.Equal(TenantStatusEnum.Pending, response.TenantStatus);
        }
        
        [Fact]
        public async Task Create_WhenCalledIfRequestHasNoName_ShouldThrowRequestValidationException()
        {
            // Arrange
            _context = TestDbCreator.GetApplicationTestDbContext(_serviceProvider);
            TestDbCreator.CreateDatabase(_context);
            var target = TestDependenciesResolver.GetService<ICreateTenantCommand>(_serviceProvider);
            
            var tenantRequestDto = new CreateTenantRequestDto()
            {
                Name = string.Empty,
                LogoUrl = string.Empty,
                CurrencyId = CurrencyEnum.UsDollars
            };

            // Act && Assert
            await Assert.ThrowsAsync<RequestValidationException>(
                  async () => await target.ExecuteAsync(tenantRequestDto));
        }
        
        [Fact]
        public async Task Create_WhenCalledWithExistentTenantNameInDb_ShouldThrowRequestValidationException()
        {
            // Arrange
            _context = TestDbCreator.GetApplicationTestDbContext(_serviceProvider);
            TestDbCreator.CreateDatabase(_context);
            var domainValidator = TestDependenciesResolver.GetService<IValidateTenantCreation>(_serviceProvider);
            await TestSeeder.CreateDemoTenant(_context, domainValidator);
            var target = TestDependenciesResolver.GetService<ICreateTenantCommand>(_serviceProvider);
            
            var tenantRequestDto = new CreateTenantRequestDto()
            {
                Name = "Demo",
                LogoUrl = string.Empty,
                CurrencyId = CurrencyEnum.UsDollars
            };

            // Act && Assert
            await Assert.ThrowsAsync<RequestValidationException>(
                 async () => await target.ExecuteAsync(tenantRequestDto));
        }
        
        [Fact]
        public async Task Create_WhenCalledWithCurrencyId0_ShouldThrowDomainValidationException()
        {
            // Arrange
            _context = TestDbCreator.GetApplicationTestDbContext(_serviceProvider);
            TestDbCreator.CreateDatabase(_context);
            var domainValidator = TestDependenciesResolver.GetService<IValidateTenantCreation>(_serviceProvider);
            var target = TestDependenciesResolver.GetService<ICreateTenantCommand>(_serviceProvider);
            
            var tenantRequestDto = new CreateTenantRequestDto()
            {
                Name = "Demo",
                LogoUrl = string.Empty,
                CurrencyId = 0
            };

            // Act && Assert
            await Assert.ThrowsAsync<DomainValidationException>(
                 async () => await target.ExecuteAsync(tenantRequestDto));
        }

        [Fact]
        public async Task Update_WhenCalledWithValidRequest_ShouldUpdateTenantIndatabase()
        {
            // Arrange
            var context = TestDbCreator.GetApplicationTestDbContext(_serviceProvider);
            var target = TestDependenciesResolver.GetService<IUpdateTenantCommand>(_serviceProvider);
            var tenantCreationValidator = TestDependenciesResolver.GetService<IValidateTenantCreation>
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
                LogoUrl = "www.https://nothing.com"
            };
            var updatedTenantResponseDto = await target.ExecuteAsync(updateRequest);

            var insertedUpdate = context.Set<Domain.Entities.TenantAggregate.Tenant>().Single();
            // Assert
            Assert.NotNull(updatedTenantResponseDto);
            Assert.Equal(updateRequest.TenantId , updatedTenantResponseDto.TenantId);
            Assert.Equal(updateRequest.Name , updatedTenantResponseDto.Name);
            Assert.Equal(updateRequest.CurrencyId , updatedTenantResponseDto.Currency);
            Assert.Equal(updateRequest.LogoUrl , updatedTenantResponseDto.LogoUrl);
            
            Assert.Equal(updateRequest.TenantId , insertedUpdate.TenantId);
            Assert.Equal(updateRequest.Name , insertedUpdate.Name);
            Assert.Equal((int)updateRequest.CurrencyId, insertedUpdate.CurrencyId);
            Assert.Equal(updateRequest.LogoUrl, insertedUpdate.LogoUrl);
        }
    }
}