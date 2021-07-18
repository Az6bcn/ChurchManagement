using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PersonManagement.Create;
using Application.Commands.PersonManagement.Update;
using Application.Dtos.Request.Create;
using Application.Dtos.Request.Update;
using Application.RequestValidators;
using Domain.Entities.PersonAggregate;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.Commands.PersonManagement
{
    public class MemberUpdaterTest
    {
        private readonly IServiceProvider _builtServices;

        public MemberUpdaterTest()
        {
            var services = TestDependenciesResolver.AddServices();
            _builtServices = TestDependenciesResolver.BuildServices(services);
        }

        private async Task CreateMemberForRequestAsync(ApplicationDbContext context, Domain.Entities.TenantAggregate.Tenant tenant)
            => await TestSeeder.CreateDemoMember(context, tenant);

        [Fact]
        public async Task ExecuteAsync_WhenCalledWithValidRequest_ShouldUpdateMemberInDatabase()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var tenantDomainValidator
                = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
            var target = TestDependenciesResolver.GetService<IUpdateMemberCommand>(_builtServices);

            TestDbCreator.CreateDatabase(context);

            await TestSeeder.CreateDemoTenant(context, tenantDomainValidator);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

            await CreateMemberForRequestAsync(context, tenant);

            var member = context.Set<Member>().AsNoTracking().Single();

            var request = new UpdateMemberRequestDto
            {
                MemberId = member.MemberId,
                TenantId = member.TenantId,
                Name = "Demo Member Updated",
                Surname = member.Surname,
                DateAndMonthOfBirth = member.DateMonthOfBirth,
                Gender = member.Gender,
                PhoneNumber = member.PhoneNumber,
                IsWorker = true
            };

            // Act
            var response = await target.ExecuteAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.MemberId);
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.IsWorker, response.IsWorker);
        }

        [Fact]
        public async Task ExecuteAsync_WhenCalledWithInvalidRequest_ShouldThrowException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var tenantDomainValidator
                = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
            var target = TestDependenciesResolver.GetService<IUpdateMemberCommand>(_builtServices);

            TestDbCreator.CreateDatabase(context);

            await TestSeeder.CreateDemoTenant(context, tenantDomainValidator);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

            await CreateMemberForRequestAsync(context, tenant);

            var member = context.Set<Member>().AsNoTracking().Single();

            var request = new UpdateMemberRequestDto
            {
                MemberId = member.MemberId,
                TenantId = member.TenantId,
                Name = string.Empty,
                Surname = member.Surname,
                DateAndMonthOfBirth = member.DateMonthOfBirth,
                Gender = member.Gender,
                PhoneNumber = member.PhoneNumber,
                IsWorker = true
            };

            // Act and Assert
            await Assert.ThrowsAnyAsync<RequestValidationException>(async ()
                => await target.ExecuteAsync(request));
        }

        [Fact]
        public async Task ExecuteAsync_WhenCalledWithRequestWithWrongMemberId_ShouldThrowException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var tenantDomainValidator
                = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
            var target = TestDependenciesResolver.GetService<IUpdateMemberCommand>(_builtServices);

            TestDbCreator.CreateDatabase(context);

            await TestSeeder.CreateDemoTenant(context, tenantDomainValidator);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

            await CreateMemberForRequestAsync(context, tenant);

            var member = context.Set<Member>().AsNoTracking().Single();

            var request = new UpdateMemberRequestDto
            {
                MemberId = 100000,
                TenantId = member.TenantId,
                Name = string.Empty,
                Surname = member.Surname,
                DateAndMonthOfBirth = member.DateMonthOfBirth,
                Gender = member.Gender,
                PhoneNumber = member.PhoneNumber,
                IsWorker = true
            };

            // Act and Assert
            await Assert.ThrowsAnyAsync<InvalidOperationException>(async ()
                                              => await target.ExecuteAsync(request));
        }

        [Fact]
        public async Task ExecuteAsync_WhenCalledWithRequestWithWrongTenantId_ShouldThrowException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var tenantDomainValidator
                = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
            var target = TestDependenciesResolver.GetService<IUpdateMemberCommand>(_builtServices);

            TestDbCreator.CreateDatabase(context);

            await TestSeeder.CreateDemoTenant(context, tenantDomainValidator);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

            await CreateMemberForRequestAsync(context, tenant);

            var member = context.Set<Member>().AsNoTracking().Single();

            var request = new UpdateMemberRequestDto
            {
                MemberId = member.MemberId,
                TenantId = 100000,
                Name = string.Empty,
                Surname = member.Surname,
                DateAndMonthOfBirth = member.DateMonthOfBirth,
                Gender = member.Gender,
                PhoneNumber = member.PhoneNumber,
                IsWorker = true
            };

            // Act and Assert
            await Assert.ThrowsAnyAsync<InvalidOperationException>(async ()
                => await target.ExecuteAsync(request));
        }
    }
}