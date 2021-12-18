using Domain.Entities.FinanceAggregate;

namespace Application.Queries.Finances;

public interface IQueryFinance
{
    Task<Finance?> GetFinanceByIdAndTenantIdAsync(int financeId, int tenantId);
}