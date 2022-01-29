using Application.Interfaces.Repositories;
using Domain.Entities.FinanceAggregate;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class FinanceRepositoryAsync : GenericRepositoryAsync<Finance>, IFinanceRepositoryAsync
{
    private readonly ApplicationDbContext _dbContext;

    public FinanceRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Finance?> GetFinanceByIdAndTenantIdAsync(int financeId, int tenantId)
        => await _dbContext.Set<Finance>()
                           .Include(x => x.Tenant)
                           .SingleOrDefaultAsync(f => f.FinanceId == financeId 
                                                      && f.TenantId == tenantId);

    public async Task<ICollection<Finance>>
        GetFinancesBetweenDatesByTenantIdAsync(int tenantId,
                                               DateOnly startDate,
                                               DateOnly endDate)
        => await _dbContext.Set<Finance>()
                           .Where(f => f.TenantId == tenantId
                                       && f.GivenDate >= startDate.ToDateTime(new TimeOnly(0,0)) 
                                       && f.GivenDate <= endDate.ToDateTime(new TimeOnly(0,0)))
                           .ToListAsync();
}