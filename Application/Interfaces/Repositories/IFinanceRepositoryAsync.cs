using Domain.Entities.FinanceAggregate;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IFinanceRepositoryAsync : IGenericRepositoryAsync<Finance>
    {
        Task<Finance?> GetFinanceByIdAndTenantIdAsync(int financeId, int tenantId);
    }
}