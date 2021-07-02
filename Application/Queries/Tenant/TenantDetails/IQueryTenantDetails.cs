using System;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Helpers;

namespace Application.Queries.Tenant.TenantDetails
{
    public interface IQueryTenantDetails
    {
        Task<QueryResult<TenantDetailsDto?>> ExecuteAsync (Guid tenantGuidId);
    }
}