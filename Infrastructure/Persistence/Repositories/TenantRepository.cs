using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class TenantRepository: GenericRepository<Tenant>, ITenantRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TenantRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<Tenant>> GetTenantMembersByTenantGuidAsync(int tenantId) 
            => await _dbContext
            .Tenants
                .Include(t => t.Members.Where(m => m.TenantId == tenantId))
            .Where(t => t.TenantId == tenantId)
            .ToListAsync();
    }
}