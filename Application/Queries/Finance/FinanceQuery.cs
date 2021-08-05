using Application.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Application.Queries.Finance
{
    public class FinanceQuery : IQueryFinance
    {
        private readonly IFinanceRepositoryAsync _financeRepo;

        public FinanceQuery(IFinanceRepositoryAsync financeRepo)
        {
            _financeRepo =financeRepo;
        }

        public async Task<Domain.Entities.FinanceAggregate.Finance?> GetFinanceByIdAndTenantIdAsync(int financeId, int tenantId)
            => await _financeRepo.GetFinanceByIdAndTenantIdAsync(financeId, tenantId);
    }
}