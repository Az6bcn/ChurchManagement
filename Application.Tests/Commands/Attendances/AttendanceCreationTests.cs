using Application.Commands.Attendances.Create;
using Application.Dtos.Request.Create;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;
using Xunit;

namespace Application.Tests.Commands.Attendances;

public class AttendanceCreationTests
{
    private readonly IServiceProvider _builtServices;

    public AttendanceCreationTests()
    {
        var services = GetServices();
        _builtServices = TestDependenciesResolver.BuildServices(services);
    }

    private IServiceCollection GetServices() => TestDependenciesResolver.AddServices();

    private async Task CreateTenantForRequestAsync(IValidateTenantInDomain validator,
                                                   ApplicationDbContext context)
        => await TestSeeder.CreateDemoTenant(context, validator);

    [Fact]
    public async Task ExecuteAsync_WhenCalledWithValidRequest_CreatesAttendanceInDatabase()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<ICreateAttendanceCommand>(_builtServices);
        var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
        TestDbCreator.CreateDatabase(context);
        await CreateTenantForRequestAsync(validator, context);
        var tenant = await context.Set<Domain.Entities.TenantAggregate.Tenant>().SingleAsync();

        var request = new CreateAttendanceRequestDto()
        {
            TenantId = tenant.TenantId,
            ServiceDate = DateOnly.FromDateTime(DateTime.UtcNow.Date),
            Male = 27,
            Female = 89,
            Children = 23,
            NewComers = 16,
            ServiceTypeEnum = ServiceEnum.Thanksgiving
        };

        // Act
        await target.ExecuteAsync(request);

        // Assert
        var insertedFinance = await context.Set<Domain.Entities.AttendanceAggregate.Attendance>().SingleAsync();
        Assert.NotNull(insertedFinance);
    }

    [Fact]
    public async Task ExecuteAsync_WhenCalledWithInvalidTenant_ThrowsException()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<ICreateAttendanceCommand>(_builtServices);
        var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
        TestDbCreator.CreateDatabase(context);
        await CreateTenantForRequestAsync(validator, context);
        var tenant = await context.Set<Domain.Entities.TenantAggregate.Tenant>().SingleAsync();

        var request = new CreateAttendanceRequestDto()
        {
            TenantId = 99999,
            ServiceDate = DateOnly.FromDateTime(DateTime.UtcNow.Date),
            Male = 27,
            Female = 89,
            Children = 23,
            NewComers = 16,
            ServiceTypeEnum = ServiceEnum.Thanksgiving
        };

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await target.ExecuteAsync(request));
    }

    [Fact]
    public async Task ExecuteAsync_WhenCalledWithNegativeCounts_ThrowsException()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<ICreateAttendanceCommand>(_builtServices);
        var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
        TestDbCreator.CreateDatabase(context);
        await CreateTenantForRequestAsync(validator, context);
        var tenant = await context.Set<Domain.Entities.TenantAggregate.Tenant>().SingleAsync();

        var request = new CreateAttendanceRequestDto()
        {
            TenantId = tenant.TenantId,
            ServiceDate = DateOnly.FromDateTime(DateTime.UtcNow.Date),
            Male = -3,
            Female = -5,
            Children = -3,
            NewComers = -6,
            ServiceTypeEnum = ServiceEnum.Thanksgiving
        };

        // Act and Assert
        await Assert.ThrowsAsync<DomainValidationException>(async () => await target.ExecuteAsync(request));
    }

    [Fact]
    public async Task ExecuteAsync_WhenCalledWithDefaultServiceDate_ThrowsException()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<ICreateAttendanceCommand>(_builtServices);
        var validator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
        TestDbCreator.CreateDatabase(context);
        await CreateTenantForRequestAsync(validator, context);
        var tenant = await context.Set<Domain.Entities.TenantAggregate.Tenant>().SingleAsync();

        var request = new CreateAttendanceRequestDto()
        {
            TenantId = tenant.TenantId,
            Male = 27,
            Female = 89,
            Children = 23,
            NewComers = 16,
            ServiceTypeEnum = ServiceEnum.Thanksgiving
        };

        // Act and Assert
        await Assert.ThrowsAsync<DomainValidationException>(async () => await target.ExecuteAsync(request));
    }
}