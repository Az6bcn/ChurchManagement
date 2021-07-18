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
    public class MemberDeleteTests
    {
        private readonly IServiceProvider _builtServiceProvider;

        public MemberDeleteTests()
        {
            var services = ResolveServices();
            _builtServiceProvider = TestDependenciesResolver.BuildServices(services);
        }

        private IServiceCollection ResolveServices() => TestDependenciesResolver.AddServices();

        [Fact]
        public async Task ExecuteAsync_WhenCalled_ShouldMarksMemberAsDeleted()
        {
            // Arrange
            var context
                = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServiceProvider);
            var target
                = TestDependenciesResolver
                    .GetService<IDeleteMemberCommand>(_builtServiceProvider);
            var tenantValidator
                = TestDependenciesResolver
                    .GetService<IValidateTenantInDomain>(_builtServiceProvider);
            TestDbCreator.CreateDatabase(context);

            await TestSeeder.CreateDemoTenant(context, tenantValidator);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

            await TestSeeder.CreateDemoMember(context, tenant);
            var member = context.Set<Member>().Single();

            // Act
            await target.ExecuteAsync(member.MemberId, tenant.TenantId);

            // Assert
            Assert.NotNull(member.Deleted);
        }

        [Fact]
        public async Task
            ExecuteAsync_WhenCalledWithMemberThatDontBelongToTenant_ShouldThrowException()
        {
            // Arrange
            var context
                = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServiceProvider);
            var target
                = TestDependenciesResolver
                    .GetService<IDeleteMemberCommand>(_builtServiceProvider);
            var tenantValidator
                = TestDependenciesResolver
                    .GetService<IValidateTenantInDomain>(_builtServiceProvider);
            TestDbCreator.CreateDatabase(context);

            await TestSeeder.CreateDemoTenant(context, tenantValidator);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().Single();

            await TestSeeder.CreateDemoMember(context, tenant);
            var member = context.Set<Member>().Single();

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
                    .GetService<IDeleteMemberCommand>(_builtServiceProvider);
            var tenantValidator
                = TestDependenciesResolver
                    .GetService<IValidateTenantInDomain>(_builtServiceProvider);
            TestDbCreator.CreateDatabase(context);

            await TestSeeder.CreateDemoTenant(context, tenantValidator);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().Single();

            await TestSeeder.CreateDemoMember(context, tenant);
            var member = context.Set<Member>().Single();

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()
                         => await target.ExecuteAsync(member.MemberId, 100000));
        }
    }
}