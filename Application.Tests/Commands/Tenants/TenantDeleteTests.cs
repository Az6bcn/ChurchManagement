using Application.Commands.Tenants.Delete;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.Commands.Tenants;

public class TenantDeleteTests
{
    private readonly IServiceProvider _serviceProvider;

    public TenantDeleteTests()
    {
        var services = GetServices();
        _serviceProvider = TestDependenciesResolver.BuildServices(services);
    }

    private IServiceCollection GetServices() => TestDependenciesResolver.AddServices();

    [Fact]
    public async Task ExecuteAsync_WhenCalled_ShouldMarkTenantAsDeleted()
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
        
    [Fact]
    public async Task ExecuteAsync_WhenCalledWithoutTenantId_ShouldThrowException()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_serviceProvider);
        var target = TestDependenciesResolver.GetService<IDeleteTenantCommand>(_serviceProvider);

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                                                              async () => await target.ExecuteAsync(0));
    }
        
    [Fact]
    public async Task ExecuteAsync_WhenCalledWithNonExistentTenantId_ShouldThrowException()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_serviceProvider);
        var target = TestDependenciesResolver.GetService<IDeleteTenantCommand>(_serviceProvider);
        var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_serviceProvider);
        TestDbCreator.CreateDatabase(context);
        await TestSeeder.CreateDemoTenant(context, validator);
            
        // Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(
                                                    async () => await target.ExecuteAsync(1000000));
    }
}