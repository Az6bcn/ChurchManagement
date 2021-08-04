using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PersonManagement.Create;
using Application.Dtos.Request.Create;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;
using Xunit;

namespace Application.Tests.Commands.Finance
{
    public class FinanceCreationTests
    {
        private readonly IServiceProvider _builtServices;

        public FinanceCreationTests()
        {
            var services = GetServices();
            _builtServices = TestDependenciesResolver.BuildServices(services);
        }

        private IServiceCollection GetServices() => TestDependenciesResolver.AddServices();

        private async Task CreateTenantForRequestAsync(IValidateTenantInDomain validator,
                                                       ApplicationDbContext context)
            => await TestSeeder.CreateDemoTenant(context, validator);

        [Fact]
        public async Task ExecuteAsync_WhenCalledWithValidRequest_CreatesFinanceInDatabase()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<ICreateFinanceCommand>(_builtServices);
            var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            await CreateTenantForRequestAsync(validator, context);
            var tenant = await context.Set<Domain.Entities.TenantAggregate.Tenant>().SingleAsync();

            var request = new CreateFinanceRequestDto
            {
                TenantId = tenant.TenantId,
                FinanceTypeEnum = FinanceEnum.Thanksgiving,
                ServiceTypeEnum = ServiceEnum.Thanksgiving,
                CurrencyTypeEnum = CurrencyEnum.UsDollars,
                Amount = 234.5m,
                GivenDate = DateTime.Now.Date,
                Description = "Fires Thanksgiving Offering"
            };

            // Act
            await target.ExecuteAsync(request);

            // Assert
            var insertedFinance = await context.Set<Domain.Entities.FinanceAggregate.Finance>().SingleAsync();
            Assert.NotNull(insertedFinance);
            Assert.Equal(request.Description, insertedFinance.Description);
            Assert.Equal(request.Amount, insertedFinance.Amount);
        }

        [Fact]
        public async Task ExecuteAsync_WhenCalledWithInvalidTenant_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<ICreateFinanceCommand>(_builtServices);
            var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            await CreateTenantForRequestAsync(validator, context);
            var tenant = await context.Set<Domain.Entities.TenantAggregate.Tenant>().SingleAsync();

            var request = new CreateFinanceRequestDto
            {
                TenantId = 99999,
                FinanceTypeEnum = FinanceEnum.Thanksgiving,
                ServiceTypeEnum = ServiceEnum.Thanksgiving,
                CurrencyTypeEnum = CurrencyEnum.UsDollars,
                Amount = 234.5m,
                GivenDate = DateTime.Now.Date,
                Description = "Fires Thanksgiving Offering"
            };

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await target.ExecuteAsync(request));
        }
        
        [Fact]
        public async Task ExecuteAsync_WhenCalledWithNegativeAmount_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<ICreateFinanceCommand>(_builtServices);
            var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            await CreateTenantForRequestAsync(validator, context);
            var tenant = await context.Set<Domain.Entities.TenantAggregate.Tenant>().SingleAsync();

            var request = new CreateFinanceRequestDto
            {
                TenantId = tenant.TenantId,
                FinanceTypeEnum = FinanceEnum.Thanksgiving,
                ServiceTypeEnum = ServiceEnum.Thanksgiving,
                CurrencyTypeEnum = CurrencyEnum.UsDollars,
                Amount = -234.5m,
                GivenDate = DateTime.Now.Date,
                Description = "Fires Thanksgiving Offering"
            };
            
            // Act and Assert
            await Assert.ThrowsAsync<DomainValidationException>(async () => await target.ExecuteAsync(request));
        }
        
        [Fact]
        public async Task ExecuteAsync_WhenCalledWithDefaultGivenDate_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<ICreateFinanceCommand>(_builtServices);
            var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            await CreateTenantForRequestAsync(validator, context);
            var tenant = await context.Set<Domain.Entities.TenantAggregate.Tenant>().SingleAsync();

            var request = new CreateFinanceRequestDto
            {
                TenantId = tenant.TenantId,
                FinanceTypeEnum = FinanceEnum.Thanksgiving,
                ServiceTypeEnum = ServiceEnum.Thanksgiving,
                CurrencyTypeEnum = CurrencyEnum.UsDollars,
                Amount = 234.5m,
                Description = "Fires Thanksgiving Offering"
            };
            
            // Act and Assert
            await Assert.ThrowsAsync<DomainValidationException>(async () => await target.ExecuteAsync(request));
        }
    }
}