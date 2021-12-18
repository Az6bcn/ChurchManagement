using Application.Dtos.Response.Get;
using Application.Helpers;
using Domain.Entities.PersonAggregate;

namespace Application.Queries.PersonManagements;

public interface IQueryPersonManagement
{
    // Person, Member, NewComer, Department 
    Task<IEnumerable<string?>> GetDepartmentNamesByTenantIdAsync(int tenantId);
    Task<IEnumerable<Department>> GetTenantDepartmentsByTenantIdAsync(int tenantId);
    Task<QueryResult<GetDepartmentsResponseDto>> GetDepartmentsByTenantIdAsync(int tenantId);

    Task<Department?> GetDepartmentIdAsync(int departmentId, int tenantId);

    Task<Member?> GetMemberByIdAsync(int memberId, int tenantId);
    Task<QueryResult<GetMembersResponseDto>> GetMembersByTenantIdAsync(int tenantId, bool onlyWorkers = false);

    Task<NewComer?> GetNewComerByIdAsync(int newComerId,
                                         int tenantId);

    Task<QueryResult<GetNewComersResponseDto>> GetNewComersByTenantIdAsync(int tenantId);

    Task<Minister?> GetMinisterByIdAsync(int ministerId,
                                         int tenantId);

    Task<QueryResult<GetMinistersResponseDto>> GetMinistersByTenantIdAsync(int tenantId);

    public Task<DepartmentMembers?> GetDepartmentMemberAsync(int departmentId,
                                                             int memberId,
                                                             int tenantId);
}