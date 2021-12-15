using Domain.Entities.TenantAggregate;

namespace Application.Queries.Tenants;

public interface IQueryTenant
{
    Task<IEnumerable<Tenant>> GetTenantsAsync();
    Task<IEnumerable<string>> GetTenantNamesAsync();
    Task<Tenant?> GetTenantByIdAsync(int id);
    Task<Tenant?> GetTenantByGuidIdAsync(Guid id);
}