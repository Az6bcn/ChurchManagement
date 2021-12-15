using Application.Commands.PersonManagements.Create;
using Application.Commands.PersonManagements.Update;
using Application.Dtos.Request.Create;
using Application.Dtos.Request.Update;
using Application.RequestValidators;
using Domain.Entities.PersonAggregate;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;
using Xunit;

namespace Application.Tests.Commands.PersonManagements;

public class NewComerUpdaterTests
{
    private readonly IServiceProvider _builtServices;

    public NewComerUpdaterTests()
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
    public async Task ExecuteAsync_WhenCalled_UpdatesNewComer()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<IUpdateNewComerCommand>(_builtServices);
        var tenantDomainValidator
            = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);

        TestDbCreator.CreateDatabase(context);

        await CreateTenantForRequestAsync(tenantDomainValidator, context);
        var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>().AsNoTracking().Single();

        await CreateNewComerForRequestAsync(context, tenant);
        var newComer = context.Set<NewComer>().AsNoTracking().Single();

        var request = new UpdateNewComerRequestDto
        {
            TenantId = tenant.TenantId,
            NewComerId = newComer.NewComerId,
            Name = "Demo new comer updated",
            Surname = newComer.Surname,
            DateAttended = newComer.DateAttended,
            DateAndMonthOfBirth = newComer.DateMonthOfBirth,
            ServiceTypeEnum = (ServiceEnum)newComer.ServiceTypeId,
            Gender = newComer.Gender,
            PhoneNumber = newComer.PhoneNumber
        };

        // Act
        var response = await target.ExecuteAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(request.NewComerId, response.NewComerId);            Assert.Equal(request.DateAttended, response.DateAttended);
        Assert.Equal(request.ServiceTypeEnum, response.ServiceTypeEnum);
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
        await Assert.ThrowsAsync<RequestValidationException>(async ()
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
        await Assert.ThrowsAsync<RequestValidationException>(async ()
                                                                 => await target.ExecuteAsync(request));
    }
        
    [Fact]
    public async Task ExecuteAsync_WhenCalledWithRequestWithDefaultSteAttended_ShouldThrowException()
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
        await Assert.ThrowsAsync<DomainValidationException>(async ()
                                                                => await target.ExecuteAsync(request));
    }
}