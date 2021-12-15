using Application.Commands.Finances.Update;
using Application.Dtos.Request.Update;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;
using Xunit;

namespace Application.Tests.Commands.Finances;

public class FinanceUpdaterTests
{
    private readonly IServiceProvider _builtServices;
    public FinanceUpdaterTests()
    {
        var services = GetServices();
        _builtServices = TestDependenciesResolver.BuildServices(services);
    }

    private IServiceCollection GetServices() => TestDependenciesResolver.AddServices();

    private async Task CreateTenantForRequestAsync(IValidateTenantInDomain tenantValidator,
                                                   IValidateFinanceInDomain financeValidator,
                                                   ApplicationDbContext context)
        => await TestSeeder.CreateDemoFinance(tenantValidator, financeValidator, context);

    [Fact]
    public async Task ExecuteAsync_WhenCalledWithValidRequest_UpdatesInDatabase()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<IUpdateFinanceCommand>(_builtServices);
        var tenantValidator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
        var financeValidator = TestDependenciesResolver.GetService<IValidateFinanceInDomain>(_builtServices);
        TestDbCreator.CreateDatabase(context);
        await CreateTenantForRequestAsync(tenantValidator, financeValidator, context);
            
        var finance = await context.Set<Domain.Entities.FinanceAggregate.Finance>().SingleAsync();
        var request = new UpdateFinanceRequestDto
        {
            FinanceId = finance.FinanceId,
            TenantId = finance.TenantId,
            FinanceTypeEnum = FinanceEnum.Thanksgiving,
            ServiceTypeEnum = ServiceEnum.Thanksgiving,
            CurrencyTypeEnum = CurrencyEnum.Naira,
            Amount = 1500m,
            GivenDate = DateOnly.FromDateTime(DateTime.Now.Date),
            Description = "Updated Thanksgiving Offering"
        };
            
        // Act
        await target.ExecuteAsync(request);

        // Assert
        var inserted = await context.Set<Domain.Entities.FinanceAggregate.Finance>().SingleAsync();
        Assert.Equal(request.Amount, inserted.Amount);
        Assert.Equal(request.Description, inserted.Description);
        Assert.Equal((int)request.CurrencyTypeEnum, inserted.CurrencyId);
        Assert.NotNull(inserted.UpdatedAt);
    }
}