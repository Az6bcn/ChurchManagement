using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities.PersonAggregate;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PersonManagementRepositoryAsync : IPersonManagementRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public PersonManagementRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<string?>> GetDepartmentNamesByTenantIdAsync(int tenantId)
        {
            var departmentNames = await _dbContext.Set<Department>()
                                                  .Include(x => x.Tenant)
                                                  .Where(d => d.TenantId == tenantId)
                                                  .Select(d => d.Name)
                                                  .ToListAsync();

            return departmentNames;
        }

        public async Task AddAsync<T>(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsByTenantIdAsync(int tenantId)
            => await _dbContext.Set<Department>()
                               .Include(x => x.Tenant)
                               .Where(d => d.TenantId == tenantId)
                               .ToListAsync();
    }
}