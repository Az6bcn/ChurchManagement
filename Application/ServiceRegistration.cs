using System.Reflection;
using Application.Commands.PersonManagement.Create;
using Application.Commands.PersonManagement.Delete;
using Application.Commands.PersonManagement.Update;
using Application.Commands.Tenant.Create;
using Application.Commands.Tenant.Delete;
using Application.Commands.Tenant.Update;
using Application.Interfaces.UnitOfWork;
using Application.Queries;
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            // Application
            services.AddScoped<ICreateTenantCommand, TenantCommandCreator>();
            services.AddScoped<IUpdateTenantCommand, TenantCommandUpdater>();
            services.AddScoped<IDeleteTenantCommand, TenantDeleteCommand>();
            services.AddScoped<IQueryTenant, TenantQuery>();
            services.AddScoped<IQueryTenantDetails, TenantDetailsQuery>();
            services.AddScoped<IQueryTenantDashboardData, TenantDashboardQuery>();
            services.AddScoped<IValidateTenantRequestDto, TenantRequestDtoValidator>();
            services.AddScoped<IValidateTenantInDomain, TenantInDomainValidator>();

            services.AddScoped<ICreateDepartmentCommand, DepartmentCommandCreator>();
            services.AddScoped<IUpdateDepartmentCommand, DepartmentCommandUpdater>();
            services.AddScoped<IDeleteDepartmentCommand, DepartmentDeleteCommand>();
            services.AddScoped<IQueryPersonManagement, PersonManagementQuery>();
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

        }
    }
}