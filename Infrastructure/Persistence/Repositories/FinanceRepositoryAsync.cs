using Application.Interfaces.Repositories;
using Domain.Entities.FinanceAggregate;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class FinanceRepositoryAsync : GenericRepositoryAsync<Finance>, IFinanceRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public FinanceRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Finance?> GetFinanceByIdAndTenantIdAsync(int financeId, int tenantId)
            => await _dbContext.Set<Finance>().SingleOrDefaultAsync(f => f.FinanceId == financeId && f.TenantId == tenantId);
    }
}