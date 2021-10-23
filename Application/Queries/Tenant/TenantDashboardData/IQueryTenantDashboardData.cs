using System;
using System.Threading.Tasks;
using Application.Dtos.Response.Get;
using Application.Helpers;

namespace Application.Queries.Tenant.TenantDashboardData
{
    public interface IQueryTenantDashboardData
    {
        Task<QueryResult<GetDashboardDataResponseDto>> ExecuteAsync(int tenantId,
                                                                    DateTime? startDate,
                                                                    DateTime? endDate);
    }
}