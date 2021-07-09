using System.Reflection;
using Application.Commands.Tenant.Create;
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
            services.AddScoped<IValidateTenantInDomain, TenantInDomainValidator>();
        
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IQueryTenantDetails, TenantDetailsQuery>();
            services.AddScoped<IQueryTenantDashboardData, TenantDashboardQuery>();
            services.AddScoped<ICreateTenantCommand, TenantCommandCreator>();
            services.AddScoped<IUpdateTenantCommand, TenantCommandUpdater>();
            services.AddScoped<IQueryTenant, TenantQuery>();
            services.AddScoped<IValidateTenantRequestDto, TenantRequestDtoValidator>();
        }
    }
}