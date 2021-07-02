using System.Reflection;
using Application.Commands.Tenant.Create;
using Application.Queries.Tenant.TenantDashboardData;
using Application.Queries.Tenant.TenantDetails;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IQueryTenantDetails, TenantDetailsQuery>();
            services.AddTransient<IQueryTenantDashboardData, TenantDashboardQuery>();
            services.AddTransient<ICreateTenantCommand, TenantCommandCreator>();
        }
    }
}