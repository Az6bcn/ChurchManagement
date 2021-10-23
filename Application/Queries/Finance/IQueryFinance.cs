using System.Threading.Tasks;

namespace Application.Queries.Finance
{
    public interface IQueryFinance
    {
        Task<Domain.Entities.FinanceAggregate.Finance?> GetFinanceByIdAndTenantIdAsync(int financeId, int tenantId);
    }
}