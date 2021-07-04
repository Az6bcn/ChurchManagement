using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Tenant.Create;
using Application.Dtos.Request.Create;
using Application.RequestValidators;
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
                TenantStatusEnum = TenantStatusEnum.Pending,
                CurrencyEnum = CurrencyEnum.UsDollars
            };

            // Act
            var response = await target.ExecuteAsync(tenantRequestDto);
            var insertedEntities = _context.ChangeTracker.Entries().ToList();
            
            // Assert
            Assert.NotNull(response);
            Assert.True(response.TenantId > 0);
            Assert.Equal(TenantStatusEnum.Pending, response.TenantStatusEnum);
        }
        
        
        [Fact]
        public async Task Create_WhenCalledWithoutNameInRequest_ShouldThrowRequestValidationException()
        {
            // Arrange
            _context = TestDbCreator.GetApplicationTestDbContext(_serviceProvider);
            TestDbCreator.CreateDatabase(_context);
            var target = TestDependenciesResolver.GetService<ICreateTenantCommand>(_serviceProvider);
            var tenantRequestDto = new CreateTenantRequestDto()
            {
                Name = string.Empty,
                LogoUrl = string.Empty,
                TenantStatusEnum = TenantStatusEnum.Pending,
                CurrencyEnum = CurrencyEnum.UsDollars
            };

            // Act && Assert
            await Assert.ThrowsAsync<RequestValidationException>(
                  async () => await target.ExecuteAsync(tenantRequestDto));
        }
        
        [Fact]
        public async Task Create_WhenCalledWithExistentTenant_ShouldThrowRequestValidationException()
        {
            // Arrange
            _context = TestDbCreator.GetApplicationTestDbContext(_serviceProvider);
            TestDbCreator.CreateDatabase(_context);
            await TestSeeder.CreateDemoTenant(_context);
            var target = TestDependenciesResolver.GetService<ICreateTenantCommand>(_serviceProvider);
            var tenantRequestDto = new CreateTenantRequestDto()
            {
                Name = "Demo",
                LogoUrl = string.Empty,
                TenantStatusEnum = TenantStatusEnum.Pending,
                CurrencyEnum = CurrencyEnum.UsDollars
            };

            // Act && Assert
            await Assert.ThrowsAsync<RequestValidationException>(
                 async () => await target.ExecuteAsync(tenantRequestDto));
        }
    }
}