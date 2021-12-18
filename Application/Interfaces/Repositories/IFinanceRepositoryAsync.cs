using Domain.Entities.FinanceAggregate;

namespace Application.Interfaces.Repositories;

public interface IFinanceRepositoryAsync : IGenericRepositoryAsync<Finance>
{
    Task<Finance?> GetFinanceByIdAndTenantIdAsync(int financeId,
                                                  int tenantId);

    Task<ICollection<Finance>> GetFinancesBetweenDatesByTenantIdAsync(int tenantId,
                                                                      DateOnly startDate,
                                                                      DateOnly endDate);

}