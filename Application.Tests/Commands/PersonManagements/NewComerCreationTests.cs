using Application.Commands.PersonManagements.Create;
using Application.Dtos.Request.Create;
using Application.Exceptions;
using Application.RequestValidators;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;
using Xunit;

namespace Application.Tests.Commands.PersonManagements;

public class NewComerCreationTests
{
    private readonly IServiceProvider _builtServices;

    public NewComerCreationTests()
    {
        var services = GetServices();
        _builtServices = TestDependenciesResolver.BuildServices(services);
    }

    private IServiceCollection GetServices() => TestDependenciesResolver.AddServices();

    private async Task CreateTenantForRequestAsync(IValidateTenantInDomain validator,
                                                   ApplicationDbContext context)
        => await TestSeeder.CreateDemoTenant(context, validator);

    [Fact]
    public async Task ExecuteAsync_WhenCalled_CreatesNewComer()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<ICreateNewComerCommand>(_builtServices);
        var tenantDomainValidator
            = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);

        TestDbCreator.CreateDatabase(context);

        await CreateTenantForRequestAsync(tenantDomainValidator, context);
        var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

        var request = new CreateNewComerRequestDto
        {
            TenantId = tenant.TenantId,
            Name = "New Comer",
            Surname = "Comer",
            DateAttended = DateTime.UtcNow,
            DateAndMonthOfBirth = "16/03",
            ServiceTypeEnum = ServiceEnum.SundayService,
            Gender = "Male",
            PhoneNumber = "+447700000000"
        };

        // Act
        var response = await target.ExecuteAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.NewComerId > 0);
        Assert.Equal(request.DateAttended, response.DateAttended);
        Assert.Equal(request.ServiceTypeEnum, response.ServiceType);
    }

    [Fact]
    public async Task ExecuteAsync_WhenCalledWithIncompleteRequest_ShouldThrowException()
    {
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<ICreateNewComerCommand>(_builtServices);
        var tenantDomainValidator
            = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);

        TestDbCreator.CreateDatabase(context);

        await CreateTenantForRequestAsync(tenantDomainValidator, context);
        var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

        var request = new CreateNewComerRequestDto()
        {
            TenantId = tenant.TenantId,
            Name = "Sergio",
            Surname = "Ramos",
            DateAttended = DateTime.UtcNow,
            DateAndMonthOfBirth = "16/03",
            ServiceTypeEnum = ServiceEnum.SundayService,
            Gender = "Male",
            PhoneNumber = "07700000000"
        };

        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(async ()
                                                                 => await target.ExecuteAsync(request));
    }
        
        
    [Fact]
    public async Task ExecuteAsync_WhenCalledWithRequestWithInvalidPhoneNumber_ShouldThrowException()
    {
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<ICreateNewComerCommand>(_builtServices);
        var tenantDomainValidator
            = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);

        TestDbCreator.CreateDatabase(context);

        await CreateTenantForRequestAsync(tenantDomainValidator, context);
        var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

        var request = new CreateNewComerRequestDto()
        {
            TenantId = tenant.TenantId,
            Name = "Sergio",
            Surname = "Ramos",
            DateAttended = DateTime.UtcNow,
            DateAndMonthOfBirth = "16/03",
            ServiceTypeEnum = ServiceEnum.SundayService,
            Gender = "Male",
            PhoneNumber = "07700000000"
        };

        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(async ()
                                                                 => await target.ExecuteAsync(request));
    }
        
    [Fact]
    public async Task ExecuteAsync_WhenCalledWithRequestWithDefaultDateAttended_ShouldThrowException()
    {
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<ICreateNewComerCommand>(_builtServices);
        var tenantDomainValidator
            = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);

        TestDbCreator.CreateDatabase(context);

        await CreateTenantForRequestAsync(tenantDomainValidator, context);
        var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

        var request = new CreateNewComerRequestDto()
        {
            TenantId = tenant.TenantId,
            Name = "Sergio",
            Surname = "Ramos",
            DateAndMonthOfBirth = "16/03",
            ServiceTypeEnum = ServiceEnum.SundayService,
            Gender = "Male",
            PhoneNumber = "+447700000000"
        };

        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(async ()
                                                          => await target.ExecuteAsync(request));
    }
}