using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITenantRepositoryAsync, TenantRepositoryAsync>();
            services.AddScoped<IMemberRepositoryAsync, MemberRepositoryAsync>();
            services.AddScoped<IDepartmentReporsitory, DepartmentRepositoryAsync>();
        }
    }
}