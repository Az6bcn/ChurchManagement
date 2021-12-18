using Application.Commands.PersonManagements.Create;
using Application.Dtos.Request.Create;
using Application.RequestValidators;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.Commands.PersonManagements;

public class MemberCreationTests
{
    private readonly IServiceProvider _builtServices;

    public MemberCreationTests()
    {
        var services = GetServices();
        _builtServices = TestDependenciesResolver.BuildServices(services);
    }

    private IServiceCollection GetServices() => TestDependenciesResolver.AddServices();

    private async Task CreateTenantForRequestAsync(IValidateTenantInDomain validator,
                                                   ApplicationDbContext context)
        => await TestSeeder.CreateDemoTenant(context, validator);

    [Fact]
    public async Task ExecuteAsync_WhenCalledWithValidRequest_CreatesMemberInDb()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var tenantDomainValidator
            = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
        var target = TestDependenciesResolver.GetService<ICreateMemberCommand>(_builtServices);

        TestDbCreator.CreateDatabase(context);

        await CreateTenantForRequestAsync(tenantDomainValidator, context);
        var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

        var request = new CreateMemberRequestDto
        {
            TenantId = tenant.TenantId,
            Name = "Sergio`",
            Surname = "Messi",
            DateAndMonthOfBirth = "16/03",
            Gender = "Male",
            PhoneNumber = "+447700000000",
            IsWorker = false
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
    public async Task ExecuteAsync_WhenCalledWithIncompleteRequest_ShouldThrowException()
    {
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<ICreateMemberCommand>(_builtServices);
        var tenantDomainValidator
            = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);

        TestDbCreator.CreateDatabase(context);

        await CreateTenantForRequestAsync(tenantDomainValidator, context);
        var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

        var request = new CreateMemberRequestDto
        {
            TenantId = tenant.TenantId,
            Name = string.Empty,
            Surname = "Ramos",
            DateAndMonthOfBirth = "16/03",
            Gender = "Male",
            PhoneNumber = "+447700000000",
            IsWorker = false
        };

        // Act and Assert
        await Assert.ThrowsAsync<RequestValidationException>(async ()
                                                                 => await target.ExecuteAsync(request));
    }

    [Fact]
    public async Task ExecuteAsync_WhenCalledWithRequestWithInvalidPhoneNumber_ShouldThrowException()
    {
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<ICreateMemberCommand>(_builtServices);
        var tenantDomainValidator
            = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);

        TestDbCreator.CreateDatabase(context);

        await CreateTenantForRequestAsync(tenantDomainValidator, context);
        var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

        var request = new CreateMemberRequestDto
        {
            TenantId = tenant.TenantId,
            Name = "Sergio",
            Surname = "Ramos",
            DateAndMonthOfBirth = "16/03",
            Gender = "Male",
            PhoneNumber = "07700000000",
            IsWorker = false
        };

        // Act and Assert
        await Assert.ThrowsAsync<RequestValidationException>(
                                                             async ()
                                                                 => await target.ExecuteAsync(request));
    }
}