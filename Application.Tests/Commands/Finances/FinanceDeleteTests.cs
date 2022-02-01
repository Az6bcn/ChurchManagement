using Application.Commands.Finances.Delete;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.Commands.Finances;

public class FinanceDeleteTests
{
    private readonly IServiceProvider _builtServices;
    public FinanceDeleteTests()
    {
        var services = GetServices();
        _builtServices = TestDependenciesResolver.BuildServices(services);
    }

    private IServiceCollection GetServices() => TestDependenciesResolver.AddServices();

    private async Task CreateTenantForRequestAsync(IValidateTenantInDomain tenantValidator,
                                                   ApplicationDbContext context)
        => await TestSeeder.CreateDemoFinance(tenantValidator, context);

    [Fact]
    public async Task ExecuteAsync_WhenCalledWithValidRequest_MarksAsDeletedInDatabase()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<IDeleteFinanceCommand>(_builtServices);
        var tenantValidator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
        TestDbCreator.CreateDatabase(context);
        await CreateTenantForRequestAsync(tenantValidator, context);

        var finance = await context.Set<Domain.Entities.FinanceAggregate.Finance>().SingleAsync();
          
        // Act
        await target.ExecuteAsync(finance.FinanceId, finance.TenantId);

        // Assert
        Assert.NotNull(finance.Deleted);
    }

    [Fact]
    public async Task ExecuteAsync_WhenCalledWithValidRequestWithInvalidTenantId_ThrowsException()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<IDeleteFinanceCommand>(_builtServices);
        var tenantValidator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
        TestDbCreator.CreateDatabase(context);
        await CreateTenantForRequestAsync(tenantValidator,  context);

        var finance = await context.Set<Domain.Entities.FinanceAggregate.Finance>().SingleAsync();

        // Act and Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await target.ExecuteAsync(finance.FinanceId, 9999));

    }


    [Fact]
    public async Task ExecuteAsync_WhenCalledWithValidRequestWithInvalidFinanceId_ThrowsException()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<IDeleteFinanceCommand>(_builtServices);
        var tenantValidator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
        TestDbCreator.CreateDatabase(context);
        await CreateTenantForRequestAsync(tenantValidator,  context);

        var finance = await context.Set<Domain.Entities.FinanceAggregate.Finance>().SingleAsync();

        // Act and Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await target.ExecuteAsync(99999, finance.TenantId));

    }
}