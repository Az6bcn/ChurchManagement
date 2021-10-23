using System;
using System.Threading.Tasks;
using Application.Dtos.Response.Get;
using Application.Helpers;

namespace Application.Queries.Tenant.TenantDetails
{
    public interface IQueryTenantDetails
    {
        Task<QueryResult<GetTenantsResponseDto?>> ExecuteAsync (Guid tenantGuidId);
        Task<QueryResult<GetTenantsResponseDto?>> ExecuteAsync (int tenantId);
    }
}