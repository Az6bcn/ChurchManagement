using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ITenantRepository, TenantRepository>();
            services.AddTransient<IMemberRepository, MemberRepository>();
            services.AddTransient<IDepartmentReporsitory, DepartmentRepository>();
        }
    }
}