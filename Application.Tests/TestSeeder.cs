using Application.Dtos.Request.Create;
using Domain.Entities.AttendanceAggregate;
using Domain.Entities.FinanceAggregate;
using Domain.Entities.PersonAggregate;
using Domain.Entities.TenantAggregate;
using Domain.Validators;
using Domain.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using PersonManagementAggregate = Domain.Entities.PersonAggregate.PersonManagement;

namespace Application.Tests;

public class TestSeeder
{
    public static async Task CreateDemoTenant(ApplicationDbContext context,
                                              IValidateTenantInDomain domainValidator)
    {
        var demoTenant = Tenant.Create("Demo",
                                       string.Empty,
                                       CurrencyEnum.UsDollars,
                                       domainValidator,
                                       out IDictionary<string, object> errors);

        await context.AddAsync(demoTenant);
        await TestDbCreator.SaveChangesAsync(context);
    }

    public static async Task CreateDemoDepartment(ApplicationDbContext context,
                                                  Tenant tenant)
    {
        PersonManagementAggregate.CreateDepartment("Demo Department", tenant);

        context.ChangeTracker.Clear();

        context.Update(PersonManagementAggregate.Department);
        await TestDbCreator.SaveChangesAsync(context);
    }

    public static async Task CreateDemoMember(ApplicationDbContext context,
                                              Tenant tenant)
    {
        var person = Person.Create(tenant.TenantId,
                                   "Demo Member",
                                   "Demo Surname",
                                   "17/03",
                                   "Male",
                                   "+7703000000");

        PersonManagementAggregate.CreateMember(person,
                                               tenant,
                                               false);

        context.ChangeTracker.Clear();

        context.Update(PersonManagementAggregate.Member);
        await TestDbCreator.SaveChangesAsync(context);
    }

    public static async Task CreateDemoNewComer(ApplicationDbContext context,
                                                Tenant tenant)
    {
        var person = Person.Create(tenant.TenantId,
                                   "Demo Member",
                                   "Demo Surname",
                                   "17/03",
                                   "Male",
                                   "+7703000000");

        PersonManagementAggregate.CreateNewComer(person,
                                                 DateTime.UtcNow,
                                                 ServiceEnum.SundayService,
                                                 tenant);

        //context.ChangeTracker.Clear();

        context.Update(PersonManagementAggregate.NewComer);
        await TestDbCreator.SaveChangesAsync(context);
    }

    public static async Task<AssignMemberToDepartmentRequestDto>
        CreateDemoDepartmentMemberAsync(ApplicationDbContext context,
                                        IValidateTenantInDomain validator,
                                        bool isHod)
    {
        await TestSeeder.CreateDemoTenant(context, validator);
        var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>()
                            .AsNoTracking()
                            .Single();

        await TestSeeder.CreateDemoMember(context, tenant);

        await TestSeeder.CreateDemoDepartment(context, tenant);
        context.ChangeTracker.Clear();
        var member = context.Set<Member>()
                            .AsNoTracking()
                            .Single();

        var department = context.Set<Department>()
                                .AsNoTracking()
                                .Single();

        context.ChangeTracker.Clear();
        var departs = new DepartmentMembers(member.MemberId,
                                            department.DepartmentId,
                                            isHod,
                                            DateTime.UtcNow);

        await context.AddAsync(departs);
        await TestDbCreator.SaveChangesAsync(context);

        var request = new AssignMemberToDepartmentRequestDto
        {
            TenantId = tenant.TenantId,
            DepartmentId = department.DepartmentId,
            MemberId = member.MemberId,
            IsHeadOfDepartment = isHod
        };

        return request;
    }

    public static async Task CreateDemoFinance(IValidateTenantInDomain tenantValidator,
                                               IValidateFinanceInDomain financeValidator,
                                               ApplicationDbContext context)
    {
        await CreateDemoTenant(context, tenantValidator);
        var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>()
                            .AsNoTracking()
                            .Single();
        context.ChangeTracker.Clear();

        var finance = Finance.Create(financeValidator,
                                     tenant,
                                     50m,
                                     FinanceEnum.Thanksgiving,
                                     ServiceEnum.SundayService,
                                     CurrencyEnum.UsDollars,
                                     DateOnly.FromDateTime(DateTime.Now),
                                     "Demo");

        context.ChangeTracker.Clear();

        context.Update(finance);
        await TestDbCreator.SaveChangesAsync(context);
    }

    public static async Task CreateDemoAttendance(IValidateTenantInDomain tenantValidator,
                                                  IValidateAttendanceInDomain attendanceValidator,
                                                  ApplicationDbContext context)
    {
        await CreateDemoTenant(context, tenantValidator);
        var tenant = context.Set<Domain.Entities.TenantAggregate.Tenant>()
                            .AsNoTracking()
                            .Single();
        context.ChangeTracker.Clear();

        var attendance = Attendance.Create(attendanceValidator,
                                           tenant,
                                           DateOnly.FromDateTime(DateTime.Now),
                                           20,
                                           30,
                                           17,
                                           20,
                                           ServiceEnum.SundayService);

        context.ChangeTracker.Clear();

        context.Update(attendance);
        await TestDbCreator.SaveChangesAsync(context);
    }
}