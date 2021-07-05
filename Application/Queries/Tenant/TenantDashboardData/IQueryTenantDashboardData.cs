using System;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Helpers;

namespace Application.Queries.Tenant.TenantDashboardData
{
    public interface IQueryTenantDashboardData
    {
        Task<QueryResult<DashboardDataDto>> ExecuteAsync(Guid tenantGuidId);
    }
}