using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos.Response.Get;
using Application.Helpers;
using Domain.Entities.PersonAggregate;

namespace Application.Queries.PersonManagement
{
    public interface IQueryPersonManagement
    {
        Task<IEnumerable<string?>> GetDepartmentNamesByTenantIdAsync(int tenantId);
        Task<QueryResult<GetDepartmentsResponseDto>> GetDepartmentsByTenantIdAsync(int tenantId);
    }
}