using Application.Dtos.Response.Get;
using Application.Helpers;

namespace Application.Queries.Tenants.TenantDetails;

public interface IQueryTenantDetails
{
    Task<QueryResult<GetTenantsResponseDto?>> ExecuteAsync (Guid tenantGuidId);
    Task<QueryResult<GetTenantsResponseDto?>> ExecuteAsync (int tenantId);
}