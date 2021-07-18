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
        void Update<T>(T entity);
        Task<IEnumerable<Department>> GetDepartmentsByTenantIdAsync(int tenantId);

        Task<Department?> GetDepartmentIdAsync(int departmentId, int tenantId);

        Task<Member?> GetMemberByIdAsync(int memberId, int tenantId);
        Task<IEnumerable<Member>> GetMembersByTenantIdAsync(int tenantId);
    }
}