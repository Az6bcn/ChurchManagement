using Application.Dtos.Response.Get;
using Application.Helpers;

namespace Application.Queries.Tenants.TenantDashboardData;

public interface IQueryTenantDashboardData
{
    Task<QueryResult<GetDashboardDataResponseDto>> ExecuteAsync(int tenantId,
                                                                string currencyCode,
                                                                DateTime? startDate,
                                                                DateTime? endDate);
}