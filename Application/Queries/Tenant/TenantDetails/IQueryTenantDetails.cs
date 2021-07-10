using System;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Response.Get;
using Application.Helpers;

namespace Application.Queries.Tenant.TenantDetails
{
    public interface IQueryTenantDetails
    {
        Task<QueryResult<GetTenantResponseDto?>> ExecuteAsync (Guid tenantGuidId);
        Task<QueryResult<GetTenantResponseDto?>> ExecuteAsync (int tenantId);
    }
}