using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities.PersonAggregate;

namespace Application.Queries.PersonManagement
{
    public class PersonManagementQuery : IQueryPersonManagement
    {
        private readonly IPersonManagementRepositoryAsync _personManagementRepo;

        public PersonManagementQuery(IPersonManagementRepositoryAsync personManagementRepo)
        {
            _personManagementRepo = personManagementRepo;
        }

        public async Task<IEnumerable<string?>> GetDepartmentNamesByTenantIdAsync(int tenantId)
            => await _personManagementRepo.GetDepartmentNamesByTenantIdAsync(tenantId);
    }
}