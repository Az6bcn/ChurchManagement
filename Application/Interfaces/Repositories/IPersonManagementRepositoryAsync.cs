using Domain.Entities.PersonAggregate;

namespace Application.Interfaces.Repositories;

public interface IPersonManagementRepositoryAsync
{
    // Person, Member, NewComer, Department 

    Task<IEnumerable<string?>> GetDepartmentNamesByTenantIdAsync(int tenantId);
    Task AddAsync<T>(T entity);
    void Update<T>(T entity);
    Task<IEnumerable<Department>> GetDepartmentsByTenantIdAsync(int tenantId);

    Task<Department?> GetDepartmentIdAsync(int departmentId,
                                           int tenantId);

    Task<Member?> GetMemberByIdAsync(int memberId,
                                     int tenantId);

    Task<IEnumerable<Member>> GetMembersByTenantIdAsync(int tenantId, bool onlyWorkers = false);

    Task<NewComer?> GetNewComerByIdAsync(int newComerId,
                                         int tenantId);

    Task<IEnumerable<NewComer>> GetNewComersByTenantIdAsync(int tenantId);

    Task<Minister?> GetMinisterByIdAsync(int ministerId,
                                         int tenantId);

    Task<IEnumerable<Minister>> GetMinistersByTenantIdAsync(int tenantId);

    Task<DepartmentMembers?> GetDepartmentMemberAsync(int departmentId,
                                                      int memberId,
                                                      int tenantId);

    Task<(int members, int newComers)> GetPersonsBetweenDatesByTenantIdAsync(int tenantId);
}