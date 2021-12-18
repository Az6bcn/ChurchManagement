using Application.Commands.Attendances.Delete;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.Commands.Attendances;

public class AttendanceDeleteTests
{
    private readonly IServiceProvider _builtServices;
    public AttendanceDeleteTests()
    {
        var services = GetServices();
        _builtServices = TestDependenciesResolver.BuildServices(services);
    }

    private IServiceCollection GetServices() => TestDependenciesResolver.AddServices();

    private async Task CreateTenantForRequestAsync(IValidateTenantInDomain tenantValidator,
                                                   IValidateAttendanceInDomain attendanceValidator,
                                                   ApplicationDbContext context)
        => await TestSeeder.CreateDemoAttendance(tenantValidator, attendanceValidator, context);

    [Fact]
    public async Task ExecuteAsync_WhenCalledWithValidRequest_MarksAsDeletedInDatabase()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<IDeleteAttendanceCommand>(_builtServices);
        var tenantValidator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
        var attendanceValidator = TestDependenciesResolver.GetService<IValidateAttendanceInDomain>(_builtServices);
        TestDbCreator.CreateDatabase(context);
        await CreateTenantForRequestAsync(tenantValidator, attendanceValidator, context);

        var attendance = await context.Set<Domain.Entities.AttendanceAggregate.Attendance>().SingleAsync();
          
        // Act
        await target.ExecuteAsync(attendance.AttendanceId, attendance.TenantId);

        // Assert
        Assert.NotNull(attendance.Deleted);
    }

    [Fact]
    public async Task ExecuteAsync_WhenCalledWithValidRequestWithInvalidTenantId_ThrowsException()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<IDeleteAttendanceCommand>(_builtServices);
        var tenantValidator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
        var attendanceValidator = TestDependenciesResolver.GetService<IValidateAttendanceInDomain>(_builtServices);
        TestDbCreator.CreateDatabase(context);
        await CreateTenantForRequestAsync(tenantValidator, attendanceValidator, context);

        var attendance = await context.Set<Domain.Entities.AttendanceAggregate.Attendance>().SingleAsync();

        // Act and Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await target.ExecuteAsync(attendance.AttendanceId, 
                                                                         9999));

    }


    [Fact]
    public async Task ExecuteAsync_WhenCalledWithValidRequestWithInvalidFinanceId_ThrowsException()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<IDeleteAttendanceCommand>(_builtServices);
        var tenantValidator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
        var attendanceValidator = TestDependenciesResolver.GetService<IValidateAttendanceInDomain>(_builtServices);
        TestDbCreator.CreateDatabase(context);
        await CreateTenantForRequestAsync(tenantValidator, attendanceValidator, context);

        var attendance = await context.Set<Domain.Entities.AttendanceAggregate.Attendance>().SingleAsync();

        // Act and Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await target.ExecuteAsync(99999, attendance.TenantId));

    }
}