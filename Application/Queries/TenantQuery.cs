using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Queries.Tenant;

namespace Application.Queries
{
    public class TenantQuery : IQueryTenant
    {
        private readonly ITenantRepositoryAsync _tenantRepo;

        public TenantQuery(ITenantRepositoryAsync tenantRepo)
        {
            _tenantRepo = tenantRepo;
        }

        public async Task<IEnumerable<Domain.Entities.TenantAggregate.Tenant>> GetTenantsAsync() 
            => await _tenantRepo.GetAllAsync();

        public async Task<IEnumerable<string>> GetTenantNamesAsync()
        {
            var response = await _tenantRepo.GetTenantNamesAsync();

            return response;
        }

        public async Task<Domain.Entities.TenantAggregate.Tenant?> GetTenantByIdAsync(int id)
            => await _tenantRepo.GetByIdAsync(id);
        
        public async Task<Domain.Entities.TenantAggregate.Tenant?> GetTenantByGuidIdAsync(Guid id)
            => await _tenantRepo.GetByGuidAsync(id);
    }
}