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
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITenantRepositoryAsync, TenantRepositoryAsync>();
            services.AddTransient<IMemberRepositoryAsync, MemberRepositoryAsync>();
            services.AddTransient<IDepartmentReporsitory, DepartmentRepositoryAsync>();
        }
    }
}