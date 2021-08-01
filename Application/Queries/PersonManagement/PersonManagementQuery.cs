using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Response.Get;
using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities.PersonAggregate;
using Shared.Enums;

namespace Application.Queries.PersonManagement
{
    public class PersonManagementQuery : IQueryPersonManagement
    {
        // Person, Member, NewComer, Department 

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
                .Select(x => new GetDepartmentsResponseDto
                {
                    DepartmentId = x.DepartmentId,
                    Name = x.Name,
                    TenantId = x.TenantId
                });

            return QueryResult<GetDepartmentsResponseDto>.CreateQueryResults(response);
        }

        public async Task<Department?> GetDepartmentIdAsync(int departmentId,
                                                            int tenantId)
            => await _personManagementRepo.GetDepartmentIdAsync(departmentId, tenantId);


        public async Task<Member?> GetMemberByIdAsync(int memberId,
                                                      int tenantId)
            => await _personManagementRepo.GetMemberByIdAsync(memberId, tenantId);

        public async Task<QueryResult<GetMembersResponseDto>> GetMembersByTenantIdAsync(int tenantId)
        {
            var members = await _personManagementRepo.GetMembersByTenantIdAsync(tenantId);

            var response = members
                .Select(x => new GetMembersResponseDto
                {
                    MemberId = x.MemberId,
                    Name = x.Name,
                    Surname = x.Surname,
                    DateAndMonthOfBirth = x.DateMonthOfBirth,
                    Gender = x.Gender,
                    IsWorker = x.IsWorker,
                    PhoneNumber = x.PhoneNumber,
                    FullName = x.FullName
                });

            return QueryResult<GetMembersResponseDto>.CreateQueryResults(response);
        }

        public async Task<QueryResult<GetNewComersResponseDto>> GetNewComersByTenantIdAsync(int tenantId)
        {
            var newComers = await _personManagementRepo.GetNewComersByTenantIdAsync(tenantId);

            var response = newComers
                .Select(x => new GetNewComersResponseDto
                {
                    NewComerId = x.NewComerId,
                    Name = x.Name,
                    Surname = x.Surname,
                    DateAndMonthOfBirth = x.DateMonthOfBirth,
                    Gender = x.Gender,
                    DateAttended = x.DateAttended,
                    PhoneNumber = x.PhoneNumber,
                    ServiceType = (ServiceEnum) x.ServiceTypeId
                });

            return QueryResult<GetNewComersResponseDto>.CreateQueryResults(response);
        }

        public async Task<NewComer?> GetNewComerByIdAsync(int newComerId,
                                                          int tenantId)
            => await _personManagementRepo.GetNewComerByIdAsync(newComerId, tenantId);


        public async Task<Minister?> GetMinisterByIdAsync(int ministerId,
                                                          int tenantId)
            => await _personManagementRepo.GetMinisterByIdAsync(ministerId, tenantId);

        public async Task<QueryResult<GetMinistersResponseDto>> GetMinistersByTenantIdAsync(int tenantId)
        {
            var ministers = await _personManagementRepo.GetMinistersByTenantIdAsync(tenantId);

            var response = ministers
                .Select(m => new GetMinistersResponseDto
                {
                    MinisterId = m.MinisterId,
                    MinisterTitle = (MinisterTitleEnum) m.MinisterTitleId,
                    TenantId = m.TenantId,
                    MemberId = m.MemberId,
                    Name = m.Member.Name,
                    Surname = m.Member.Surname,
                    DateMonthOfBirth = m.Member.DateMonthOfBirth,
                    Gender = m.Member.Gender,
                    PhoneNumber = m.Member.PhoneNumber
                });

            return QueryResult<GetMinistersResponseDto>.CreateQueryResults(response);
        }

        public async Task<DepartmentMembers> GetDepartmentMemberAsync(int departmentId,
                                                                      int memberId,
                                                                      int tenantId)
            => await _personManagementRepo.GetDepartmentMemberAsync(departmentId, memberId, tenantId);
    }
}