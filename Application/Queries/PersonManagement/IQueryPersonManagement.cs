using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.PersonAggregate;

namespace Application.Queries.PersonManagement
{
    public interface IQueryPersonManagement
    {
        Task<IEnumerable<string?>> GetDepartmentNamesByTenantIdAsync(int tenantId);
    }
}