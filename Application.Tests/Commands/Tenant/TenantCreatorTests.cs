using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Tenant.Create;
using Application.Dtos.Request.Create;
using Application.RequestValidators;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;
using Xunit;

namespace Application.Tests.Commands.Tenant
{
    public class TenantCreatorTests
    {
        private ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        
        public TenantCreatorTests()
        {
            var services = ResolveServices();
            _serviceProvider = TestDependenciesResolver.BuildServices(services);
        }

        private IServiceCollection ResolveServices() => TestDependenciesResolver.AddServices();
        

        [Fact]
        public async Task ExecuteAsync_WhenCalledWithValidRequest_ShouldCreateTenantInDatabase()
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
        public async Task ExecuteAsync_WhenCalledIfRequestHasNoName_ShouldThrowRequestValidationException()
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
        public async Task ExecuteAsync_WhenCalledWithExistentTenantNameInDb_ShouldThrowRequestValidationException()
        {
            // Arrange
            _context = TestDbCreator.GetApplicationTestDbContext(_serviceProvider);
            TestDbCreator.CreateDatabase(_context);
            var domainValidator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_serviceProvider);

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
        public async Task ExecuteAsync_WhenCalledWithCurrencyId0_ShouldThrowDomainValidationException()
        {
            // Arrange
            _context = TestDbCreator.GetApplicationTestDbContext(_serviceProvider);
            TestDbCreator.CreateDatabase(_context);
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
    }
}