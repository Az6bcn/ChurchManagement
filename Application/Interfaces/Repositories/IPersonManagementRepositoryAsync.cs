using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.PersonAggregate;
using Domain.Interfaces;

namespace Application.Interfaces.Repositories
{
    public interface IPersonManagementRepositoryAsync
    {
        // Person, Member, NewComer, Department 

        Task<IEnumerable<string?>> GetDepartmentNamesByTenantIdAsync(int tenantId);
        Task AddAsync<T>(T entity);
    }
}