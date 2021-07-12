using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Tenant.Delete;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.Commands.Tenant
{
    public class TenantDeleteTests
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;

        public TenantDeleteTests()
        {
            _services = GetServices();
            _serviceProvider = TestDependenciesResolver.BuildServices(_services);
        }

        private IServiceCollection GetServices() => TestDependenciesResolver.AddServices();

        [Fact]
        public async Task Delete_WhenCalled_ShouldMarkTenantAsDeleted()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_serviceProvider);
            var target = TestDependenciesResolver.GetService<IDeleteTenantCommand>(_serviceProvider);
            var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_serviceProvider);
            TestDbCreator.CreateDatabase(context);
            await TestSeeder.CreateDemoTenant(context, validator);
            
            // Act
            var insertedTenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().Single();
            await target.ExecuteAsync(insertedTenant.TenantId);
            
            // Assert
            Assert.NotNull(insertedTenant.Deleted);
        }
    }
}