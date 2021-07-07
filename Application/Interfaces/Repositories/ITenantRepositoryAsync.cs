using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.TenantAggregate;
using Domain.ProjectionEntities;

namespace Application.Interfaces.Repositories
{
    public interface ITenantRepositoryAsync: IGenericRepositoryAsync<Tenant>
    {
        Task<IReadOnlyCollection<Tenant>> GetTenantMembersByTenantGuidAsync(int tenantId);
        Task<TenantDetailsProjection?> GetTenantByGuidIdAsync(Guid tenantGuidId);
        Task<Tenant?> GetMonthDashboardDataAsync(Guid tenantGuidId, int tenantId);
        Task<IEnumerable<string>> GetTenantNamesAsync();
    }
}