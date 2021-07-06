using System.Reflection;
using Application.Commands.Tenant.Create;
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
            services.AddTransient<IValidateTenantCreation, TenantCreationValidator>();
        
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IQueryTenantDetails, TenantDetailsQuery>();
            services.AddTransient<IQueryTenantDashboardData, TenantDashboardQuery>();
            services.AddTransient<ICreateTenantCommand, TenantCommandCreator>();
            services.AddTransient<IQueryTenant, TenantQuery>();
            services.AddTransient<IValidateTenantRequestDto, TenantRequestDtoValidator>();
        }
    }
}