using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ITenantRepository: IGenericRepository<Tenant>
    {
        Task<IReadOnlyCollection<Tenant>> GetTenantMembersByTenantGuidAsync(int tenantId);
    }
}