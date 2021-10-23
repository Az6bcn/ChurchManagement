using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PersonManagement.Delete;
using Domain.Entities.PersonAggregate;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.Commands.PersonManagement
{
    public class NewComerDeleteTests
    {
        private readonly IServiceProvider _builtServices;

        public NewComerDeleteTests()
        {
            var services = GetServices();
            _builtServices = TestDependenciesResolver.BuildServices(services);
        }

        private IServiceCollection GetServices() => TestDependenciesResolver.AddServices();

        private async Task CreateTenantForRequestAsync(IValidateTenantInDomain validator,
                                                       ApplicationDbContext context)
            => await TestSeeder.CreateDemoTenant(context, validator);

        private async Task CreateNewComerForRequestAsync(ApplicationDbContext context,
                                                         Domain.Entities.TenantAggregate.Tenant tenant)
            => await TestSeeder.CreateDemoNewComer(context, tenant);

        [Fact]
        public async Task ExecuteAsync_WhenCalled_MarksNewComerAsDeletedInDatabase()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IDeleteNewComerCommand>(_builtServices);
            var tenantDomainValidator
                = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);

            TestDbCreator.CreateDatabase(context);

            await CreateTenantForRequestAsync(tenantDomainValidator, context);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

            await CreateNewComerForRequestAsync(context, tenant);
            var newComer = context.Set<NewComer>().Single();

            // Act
            await target.ExecuteAsync(newComer.NewComerId, tenant.TenantId);

            // Assert
            Assert.NotNull(newComer.Deleted);
        }

        [Fact]
        public async Task ExecuteAsync_WhenCalledAndNewComerDontBelongToTenant_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IDeleteNewComerCommand>(_builtServices);
            var tenantDomainValidator
                = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);

            TestDbCreator.CreateDatabase(context);

            await CreateTenantForRequestAsync(tenantDomainValidator, context);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

            await CreateNewComerForRequestAsync(context, tenant);
            var newComer = context.Set<NewComer>().Single();

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()
                                                            => await target.ExecuteAsync(newComer.NewComerId,
                                                                10000));
        }
    }
}