using System;
using System.Threading.Tasks;
using Application.Commands.Tenant.Create;
using Application.Dtos.Request.Create;
using Application.Enums;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.Commands.Tenant
{
    public class TenantTests
    {
        private ApplicationTestDbContext _context;
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        
        public TenantTests()
        {
            _services = ResolveServices();
            _serviceProvider = TestDependenciesResolver.BuildServices(_services);
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
            var target = TestDependenciesResolver.GetService<ICreateTenantCommand>(_serviceProvider);
            var tenantRequestDto = new CreateTenantRequestDto()
            {
                Name = "Demo",
                LogoUrl = string.Empty,
                TenantStatusEnum = TenantStatusEnum.Pending
            };

            // Act
            var response = await target.ExecuteAsync(tenantRequestDto);

            // Assert
            Assert.NotNull(response);
        }
    }
}