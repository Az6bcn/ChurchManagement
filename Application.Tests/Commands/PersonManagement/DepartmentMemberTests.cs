using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PersonManagement.Create;
using Application.Dtos.Request.Create;
using Domain.Entities.PersonAggregate;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.Commands.PersonManagement
{
    public class DepartmentMemberTests
    {
        private readonly IServiceProvider _builtServices;

        public DepartmentMemberTests()
        {
            var services = ResolveServices();
            _builtServices = TestDependenciesResolver.BuildServices(services);
        }

        private IServiceCollection ResolveServices() => TestDependenciesResolver.AddServices();

        private async Task<AssignMemberToDepartmentRequestDto> CreateRequestAsync(ApplicationDbContext context)
        {
            var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);

            await TestSeeder.CreateDemoTenant(context, validator);
            var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>()
                                .AsNoTracking()
                                .Single();

            await TestSeeder.CreateDemoMember(context, tenant);

            await TestSeeder.CreateDemoDepartment(context, tenant);

            var member = context.Set<Member>()
                                .AsNoTracking()
                                .Single();

            var department = context.Set<Department>()
                                    .AsNoTracking()
                                    .Single();

            var request = new AssignMemberToDepartmentRequestDto
            {
                TenantId = tenant.TenantId,
                DepartmentId = department.DepartmentId,
                MemberId = member.MemberId
            };

            return request;
        }

        private async Task<AssignMemberToDepartmentRequestDto>
            CreateUnAssignRequestAsync(ApplicationDbContext context,
                                       bool isHod)
        {
            var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);

            return await TestSeeder.CreateDemoDepartmentMemberAsync(context, validator, isHod);
        }

        [Fact]
        public async Task WhenCalled_AssignsMemberToDepartment()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IAssignMemberToDepartmentCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);
            request.DateJoined = DateTime.UtcNow;

            // Act
            await target.ExecuteAsync(request);

            // Assert
            var departmentMember = context.Set<DepartmentMembers>().Single();
            Assert.NotNull(departmentMember);
            Assert.Contains(new List<DepartmentMembers> {departmentMember},
                            item => item.MemberId == request.MemberId
                                    && item.DepartmentId == request.DepartmentId);
        }
        
        [Fact]
        public async Task WhenCalled_ToAssignsMemberToDepartmentWithInvalidMemberId_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IAssignMemberToDepartmentCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);
            request.DateJoined = DateTime.UtcNow;
            request.MemberId = 99999;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()=> await target.ExecuteAsync(request));
        }
        
        [Fact]
        public async Task WhenCalled_ToAssignsMemberToDepartmentWithInvalidDepartmentId_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IAssignMemberToDepartmentCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);
            request.DateJoined = DateTime.UtcNow;
            request.DepartmentId = 99999;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()=> await target.ExecuteAsync(request));
        }
        
        [Fact]
        public async Task WhenCalled_ToAssignsMemberToDepartmentWithInvalidTenantId_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IAssignMemberToDepartmentCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);
            request.DateJoined = DateTime.UtcNow;
            request.TenantId = 99999;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()=> await target.ExecuteAsync(request));
        }
        

        [Fact]
        public async Task WhenCalled_WithRequestWithoutDateJoined_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IAssignMemberToDepartmentCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);

            // Act and Assert
            await Assert.ThrowsAsync<DomainValidationException>(async () => await target.ExecuteAsync(request));
        }

        [Fact]
        public async Task WhenCalled_UnAssignsMemberFromDepartment()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IUnAssignMemberFromDepartment>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateUnAssignRequestAsync(context, false);

            // Act
            await target.ExecuteAsync(request);

            // Assert
            var departmentMember = context.Set<DepartmentMembers>().Single(x => x.Deleted.HasValue);
            Assert.NotNull(departmentMember); // because it's marked as Deleted.
        }
        
        [Fact]
        public async Task WhenCalled_ToUnAssignsMemberToDepartmentWithInvalidMemberId_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IUnAssignMemberFromDepartment>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);
            request.DateJoined = DateTime.UtcNow;
            request.MemberId = 99999;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()=> await target.ExecuteAsync(request));
        }
        
        [Fact]
        public async Task WhenCalled_ToUnAssignsMemberToDepartmentWithInvalidDepartmentId_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IUnAssignMemberFromDepartment>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);
            request.DateJoined = DateTime.UtcNow;
            request.DepartmentId = 99999;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()=> await target.ExecuteAsync(request));
        }
        
        [Fact]
        public async Task WhenCalled_ToUnAssignsMemberToDepartmentWithInvalidTenantId_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IUnAssignMemberFromDepartment>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);
            request.DateJoined = DateTime.UtcNow;
            request.TenantId = 99999;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()=> await target.ExecuteAsync(request));
        }

        [Fact]
        public async Task WhenCalled_AssignsDepartmentHod()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IAssignHeadOfDepartmentCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateUnAssignRequestAsync(context, true);
            
            // Act
            await target.ExecuteAsync(request);

            // Assert
            var departmentMember = context.Set<DepartmentMembers>().Single();
            Assert.True(departmentMember.IsHeadOfDepartment);
        }
        
        
        [Fact]
        public async Task WhenCalled_ToAssignDepartmentHodWithInvalidMemberId_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IAssignHeadOfDepartmentCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);
            request.DateJoined = DateTime.UtcNow;
            request.MemberId = 99999;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()=> await target.ExecuteAsync(request));
        }
        
        [Fact]
        public async Task WhenCalled_ToAssignDepartmentHodWithInvalidDepartmentId_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IAssignHeadOfDepartmentCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);
            request.DateJoined = DateTime.UtcNow;
            request.DepartmentId = 99999;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()=> await target.ExecuteAsync(request));
        }
        
        [Fact]
        public async Task WhenCalled_ToAssignDepartmentHodWithInvalidTenantId_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IAssignHeadOfDepartmentCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);
            request.DateJoined = DateTime.UtcNow;
            request.TenantId = 99999;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()=> await target.ExecuteAsync(request));
        }

        [Fact]
        public async Task WhenCalled_UnAssignsDepartmentHod()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IUnAssignHeadOfDepartmentCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateUnAssignRequestAsync(context, true);
            request.IsHeadOfDepartment = false;
            
            // Act
            await target.ExecuteAsync(request);

            // Assert
            var departmentMember = context.Set<DepartmentMembers>().Single();
            Assert.False(departmentMember.IsHeadOfDepartment);
        }
        
        
        [Fact]
        public async Task WhenCalled_ToUnAssignDepartmentHodWithInvalidMemberId_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IUnAssignHeadOfDepartmentCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);
            request.DateJoined = DateTime.UtcNow;
            request.MemberId = 99999;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()=> await target.ExecuteAsync(request));
        }
        
        [Fact]
        public async Task WhenCalled_ToUnAssignDepartmentHodWithInvalidDepartmentId_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IUnAssignHeadOfDepartmentCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);
            request.DateJoined = DateTime.UtcNow;
            request.DepartmentId = 99999;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()=> await target.ExecuteAsync(request));
        }
        
        [Fact]
        public async Task WhenCalled_ToUnAssignDepartmentHodWithInvalidTenantId_ThrowsException()
        {
            // Arrange
            var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<IUnAssignHeadOfDepartmentCommand>(_builtServices);
            TestDbCreator.CreateDatabase(context);
            var request = await CreateRequestAsync(context);
            request.DateJoined = DateTime.UtcNow;
            request.TenantId = 99999;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()=> await target.ExecuteAsync(request));
        }
        
    }
}