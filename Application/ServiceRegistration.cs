using System.Reflection;
using Application.Commands.PersonManagement.Create;
using Application.Commands.Tenant.Create;
using Application.Commands.Tenant.Delete;
using Application.Commands.Tenant.Update;
using Application.Interfaces.UnitOfWork;
using Application.Queries;
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
            
            services.AddScoped<ICreateTenantCommand, TenantCommandCreator>();
            services.AddScoped<IUpdateTenantCommand, TenantCommandUpdater>();
            services.AddScoped<IDeleteTenantCommand, TenantDeleteCommand>();
            services.AddScoped<IValidateTenantInDomain, TenantInDomainValidator>();

            services.AddScoped<ICreateDepartmentCommand, DepartmentCommandCreator>();
            services.AddScoped<IValidatePersonManagementRequestDto>();
            
            services.AddScoped<IQueryTenantDetails, TenantDetailsQuery>();
            services.AddScoped<IQueryTenantDashboardData, TenantDashboardQuery>();
            services.AddScoped<IQueryTenant, TenantQuery>();
            services.AddScoped<IValidateTenantRequestDto, TenantRequestDtoValidator>();
        }
    }
}