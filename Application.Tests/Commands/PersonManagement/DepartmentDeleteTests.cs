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
    public class DepartmentDeleteTests
    {
        private readonly IServiceProvider _builtServiceProvider;

        public DepartmentDeleteTests()
        {
            var services = ResolveServices();
            _builtServiceProvider = TestDependenciesResolver.BuildServices(services);
        }

        private IServiceCollection ResolveServices() => TestDependenciesResolver.AddServices();

        [Fact]
        public async Task ExecuteAsync_WhenCalled_ShouldMarkDepartmentAsDeleted()
        {
            // Arrange
            var context
                = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServiceProvider);
            var target
                = TestDependenciesResolver
                    .GetService<IDeleteDepartmentCommand>(_builtServiceProvider);
            var tenantValidator
                = TestDependenciesResolver
                    .GetService<IValidateTenantInDomain>(_builtServiceProvider);
            TestDbCreator.CreateDatabase(context);

            await TestSeeder.CreateDemoTenant(context, tenantValidator);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

            await TestSeeder.CreateDemoDepartment(context, tenant);
            var department = context.Set<Department>().Single();

            // Act
            await target.ExecuteAsync(department.DepartmentId, tenant.TenantId);

            // Assert
            Assert.NotNull(department.Deleted);
        }

        [Fact]
        public async Task
            ExecuteAsync_WhenCalledWithDepartmentThatDontBelongToTenant_ShouldThrowException()
        {
            // Arrange
            var context
                = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServiceProvider);
            var target
                = TestDependenciesResolver
                    .GetService<IDeleteDepartmentCommand>(_builtServiceProvider);
            var tenantValidator
                = TestDependenciesResolver
                    .GetService<IValidateTenantInDomain>(_builtServiceProvider);
            TestDbCreator.CreateDatabase(context);

            await TestSeeder.CreateDemoTenant(context, tenantValidator);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().Single();

            await TestSeeder.CreateDemoDepartment(context, tenant);
            var department = context.Set<Department>().Single();

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()
                         => await target.ExecuteAsync(100000, tenant.TenantId));
        }


        [Fact]
        public async Task
            ExecuteAsync_WhenCalledWithNonExistentTenantId_ShouldThrowException()
        {
            // Arrange
            var context
                = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServiceProvider);
            var target
                = TestDependenciesResolver
                    .GetService<IDeleteDepartmentCommand>(_builtServiceProvider);
            var tenantValidator
                = TestDependenciesResolver
                    .GetService<IValidateTenantInDomain>(_builtServiceProvider);
            TestDbCreator.CreateDatabase(context);

            await TestSeeder.CreateDemoTenant(context, tenantValidator);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().Single();

            await TestSeeder.CreateDemoDepartment(context, tenant);
            var department = context.Set<Department>().Single();

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()
                         => await target.ExecuteAsync(department.DepartmentId, 100000));
        }
    }
}