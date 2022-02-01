using Application.Commands.Attendances.Update;
using Application.Dtos.Request.Update;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;
using Xunit;

namespace Application.Tests.Commands.Attendances;

public class AttendanceUpdaterTests
{
    private readonly IServiceProvider _builtServices;
    public AttendanceUpdaterTests()
    {
        var services = GetServices();
        _builtServices = TestDependenciesResolver.BuildServices(services);
    }

    private IServiceCollection GetServices() => TestDependenciesResolver.AddServices();

    private async Task CreateTenantForRequestAsync(IValidateTenantInDomain tenantValidator,
                                                   ApplicationDbContext context)
        => await TestSeeder.CreateDemoAttendance(tenantValidator, context);

    [Fact]
    public async Task ExecuteAsync_WhenCalledWithValidRequest_UpdatesInDatabase()
    {
        // Arrange
        var context = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
        var target = TestDependenciesResolver.GetService<IUpdateAttendanceCommand>(_builtServices);
        var tenantValidator = TestDependenciesResolver.GetService<IValidateTenantInDomain>(_builtServices);
        TestDbCreator.CreateDatabase(context);
        await CreateTenantForRequestAsync(tenantValidator, context);
            
        var attendance = await context.Set<Domain.Entities.AttendanceAggregate.Attendance>().SingleAsync();
        var request = new UpdateAttendanceRequestDto()
        {
            AttendanceId = attendance.AttendanceId,
            TenantId = attendance.TenantId,
            ServiceDate = DateOnly.FromDateTime(DateTime.UtcNow.Date),
            Male = 27,
            Female = 89,
            Children = 23,
            NewComers = 16,
            ServiceTypeEnum = ServiceEnum.MidWeekService
        };
            
        // Act
        await target.ExecuteAsync(request);

        // Assert
        var inserted = await context.Set<Domain.Entities.AttendanceAggregate.Attendance>().SingleAsync();
        Assert.NotNull(inserted.UpdatedAt);
    }
}