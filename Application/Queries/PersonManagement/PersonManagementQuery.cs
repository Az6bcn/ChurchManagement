using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Response.Get;
using Application.Helpers;
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
        
        public async Task<IEnumerable<Department>> GetTenantDepartmentsByTenantIdAsync(int tenantId)
            => await _personManagementRepo.GetDepartmentsByTenantIdAsync(tenantId);


        public async Task<QueryResult<GetDepartmentsResponseDto>> GetDepartmentsByTenantIdAsync(int tenantId)
        {
            var departments = await GetTenantDepartmentsByTenantIdAsync(tenantId);

            var response = departments
                .Select(x => new GetDepartmentsResponseDto());
            
            return QueryResult<GetDepartmentsResponseDto>.CreateQueryResults(response);
        }
    }
}