using System.Reflection;
using Application.Commands.Attendance.Create;
using Application.Commands.Attendance.Delete;
using Application.Commands.Attendance.Update;
using Application.Commands.Finance.Create;
using Application.Commands.Finance.Delete;
using Application.Commands.Finance.Update;
using Application.Commands.PersonManagement.Create;
using Application.Commands.PersonManagement.Delete;
using Application.Commands.PersonManagement.Update;
using Application.Commands.Tenant.Create;
using Application.Commands.Tenant.Delete;
using Application.Commands.Tenant.Update;
using Application.Queries;
using Application.Queries.Attendance;
using Application.Queries.Finance;
using Application.Queries.PersonManagement;
using Application.Queries.Tenant;
using Application.Queries.Tenant.TenantDashboardData;
using Application.Queries.Tenant.TenantDetails;
using Application.RequestValidators;
using Domain.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
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
}