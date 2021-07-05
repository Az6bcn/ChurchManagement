using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Queries.Tenant;

namespace Application.Queries
{
    public class TenantQuery: IQueryTenant
    {
        private readonly ITenantRepositoryAsync _tenantRepo;

        public TenantQuery(ITenantRepositoryAsync tenantRepo)
        {
            _tenantRepo = tenantRepo;
        }

    }
}