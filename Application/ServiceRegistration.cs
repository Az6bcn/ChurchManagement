using System.Reflection;
using Application.Commands.Attendances.Create;
using Application.Commands.Attendances.Delete;
using Application.Commands.Attendances.Update;
using Application.Commands.Finances.Create;
using Application.Commands.Finances.Delete;
using Application.Commands.Finances.Update;
using Application.Commands.PersonManagements.Create;
using Application.Commands.PersonManagements.Delete;
using Application.Commands.PersonManagements.Update;
using Application.Commands.Tenants.Create;
using Application.Commands.Tenants.Delete;
using Application.Commands.Tenants.Update;
using Application.Queries;
using Application.Queries.Attendances;
using Application.Queries.Finances;
using Application.Queries.PersonManagements;
using Application.Queries.Tenants;
using Application.Queries.Tenants.TenantDashboardData;
using Application.Queries.Tenants.TenantDetails;
using Application.RequestValidators;
using Domain.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Domain
        services.AddScoped<IValidateTenantInDomain, TenantInDomainValidator>();
        services.AddScoped<IValidateFinanceInDomain, FinanceInDomainValidator>();
        services.AddScoped<IValidateAttendanceInDomain, AttendanceInDomainValidator>();
            
        // Application
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
        services.AddScoped<ICreateTenantCommand, TenantCommandCreator>();
        services.AddScoped<IUpdateTenantCommand, TenantCommandUpdater>();
        services.AddScoped<IDeleteTenantCommand, TenantDeleteCommand>();
        services.AddScoped<IQueryTenantDetails, TenantDetailsQuery>();
        services.AddScoped<IValidateTenantRequestDto, TenantRequestDtoValidator>();
            
        services.AddScoped<IQueryTenant, TenantQuery>();
        services.AddScoped<IQueryTenantDashboardData, TenantDashboardQuery>();
        services.AddScoped<IQueryPersonManagement, PersonManagementQuery>();
        services.AddScoped<IQueryFinance, FinanceQuery>();
        services.AddScoped<IQueryAttendance, AttendanceQuery>();

        services.AddScoped<ICreateDepartmentCommand, DepartmentCommandCreator>();
        services.AddScoped<IUpdateDepartmentCommand, DepartmentCommandUpdater>();
        services.AddScoped<IDeleteDepartmentCommand, DepartmentDeleteCommand>();
        services.AddScoped<IValidatePersonManagementRequestDto, PersonManagementRequestDtoValidator>();

        services.AddScoped<ICreateMemberCommand, MemberCommandCreator>();
        services.AddScoped<IUpdateMemberCommand, MemberCommandUpdater>();
        services.AddScoped<IDeleteMemberCommand, MemberDeleteCommand>();

        services.AddScoped<ICreateNewComerCommand, NewComerCommandCreator>();
        services.AddScoped<IUpdateNewComerCommand, NewComerCommandUpdater>();
        services.AddScoped<IDeleteNewComerCommand, NewComerDeleteCommand>();

        services.AddScoped<ICreateMinisterCommand, MinisterCommandCreator>();
        services.AddScoped<IUpdateMinisterCommand, MinisterCommandUpdater>();
        services.AddScoped<IDeleteMinisterCommand, MinisterDeleteCommand>();

        services.AddScoped<IAssignMemberToDepartmentCommand, AssignMemberToDepartmentCommand>();
        services.AddScoped<IUnAssignMemberFromDepartment, UnAssignMemberFromDepartmentCommand>();
        services.AddScoped<IAssignHeadOfDepartmentCommand,AssignHeadOfDepartmentCommand>();
        services.AddScoped<IUnAssignHeadOfDepartmentCommand, UnAssignHeadOfDepartmentCommand>();

        services.AddScoped<ICreateFinanceCommand, FinanceCreatorCommand>();
        services.AddScoped<IUpdateFinanceCommand, FinanceUpdaterCommand>();
        services.AddScoped<IDeleteFinanceCommand, FinanceDeleteCommand>();

        services.AddScoped<ICreateAttendanceCommand, AttendanceCreatorCommand>();
        services.AddScoped<IUpdateAttendanceCommand, AttendanceUpdaterCommand>();
        services.AddScoped<IDeleteAttendanceCommand, AttendanceDeleteCommand>();
            
    }
}