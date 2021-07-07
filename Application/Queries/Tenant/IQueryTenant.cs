using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Queries.Tenant
{
    public interface IQueryTenant
    {
        Task<IEnumerable<Domain.Entities.TenantAggregate.Tenant>> GetTenantsAsync();
        Task<IEnumerable<string>> GetTenantNamesAsync();
        Task<Domain.Entities.TenantAggregate.Tenant?> GetTenantByIdAsync(int id);
    }
}