using Application.Interfaces.Repositories;
using Domain.Entities.FinanceAggregate;

namespace Application.Queries.Finances;

public class FinanceQuery : IQueryFinance
{
    private readonly IFinanceRepositoryAsync _financeRepo;

    public FinanceQuery(IFinanceRepositoryAsync financeRepo)
    {
        _financeRepo = financeRepo;
    }

    public async Task<Finance?> GetFinanceByIdAndTenantIdAsync(int financeId, int tenantId)
        => await _financeRepo.GetFinanceByIdAndTenantIdAsync(financeId, tenantId);
}