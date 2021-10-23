using System;
using System.Collections.Generic;
using Domain.Entities.FinanceAggregate;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IFinanceRepositoryAsync : IGenericRepositoryAsync<Finance>
    {
        Task<Finance?> GetFinanceByIdAndTenantIdAsync(int financeId,
                                                      int tenantId);

        Task<IEnumerable<Finance>> GetFinancesBetweenDatesByTenantIdAsync(int tenantId,
                                                                          DateOnly startDate,
                                                                          DateOnly endDate);

    }
}