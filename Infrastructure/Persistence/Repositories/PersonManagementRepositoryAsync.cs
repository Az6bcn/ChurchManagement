using Application.Interfaces.Repositories;
using Domain.Entities.PersonAggregate;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PersonManagementRepositoryAsync : IPersonManagementRepositoryAsync
{
    // Person, Member, NewComer, Department 

    private readonly ApplicationDbContext _dbContext;

    public PersonManagementRepositoryAsync(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<string?>> GetDepartmentNamesByTenantIdAsync(int tenantId)
        => await _dbContext.Set<Department>()
                           .Include(x => x.Tenant)
                           .Where(d => d.TenantId == tenantId)
                           .Select(d => d.Name)
                           .ToListAsync();

    public async Task AddAsync<T>(T entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public void Update<T>(T entity)
    {
        _dbContext.Update(entity);
    }


    public async Task<IEnumerable<Department>> GetDepartmentsByTenantIdAsync(int tenantId)
        => await _dbContext.Set<Department>()
                           .Include(x => x.Tenant)
                           .Where(d => d.TenantId == tenantId)
                           .ToListAsync();

    public async Task<Department?> GetDepartmentIdAsync(int departmentId, int tenantId)
        => await _dbContext.Set<Department>()
                           .Include(d => d.Tenant)
                           .SingleOrDefaultAsync(d => d.DepartmentId == departmentId
                                                      && d.TenantId == tenantId);

    public async Task<Member?> GetMemberByIdAsync(int memberId, int tenantId)
        => await _dbContext.Set<Member>()
                           .Include(m => m.Tenant)
                           .SingleOrDefaultAsync(m => m.MemberId == memberId &&
                                                      m.TenantId == tenantId);

    public async Task<IEnumerable<Member>> GetMembersByTenantIdAsync(int tenantId, bool onlyWorkers)
    { 
        var query = _dbContext.Set<Member>()
                              .Where(m => m.TenantId == tenantId);

        if (onlyWorkers)
            query = query.Where(x => x.IsWorker);
            
        return await query.ToListAsync();
    }
    public async Task<NewComer?> GetNewComerByIdAsync(int newComerId, int tenantId)
        => await _dbContext.Set<NewComer>()
                           .Include(m => m.Tenant)
                           .SingleOrDefaultAsync(nc => nc.NewComerId == newComerId
                                                       && nc.TenantId == tenantId);

    public async Task<IEnumerable<NewComer>> GetNewComersByTenantIdAsync(int tenantId)
        => await _dbContext.Set<NewComer>()
                           .Include(nc => nc.Tenant)
                           .ToListAsync();

    public async Task<Minister?> GetMinisterByIdAsync(int ministerId, int tenantId)
        => await _dbContext.Set<Minister>()
                           .Include(m => m.Member)
                           .ThenInclude(m => m.Tenant)
                           .Include(m => m.Tenant)
                           .SingleOrDefaultAsync(m => m.MinisterId == ministerId 
                                                      && m.TenantId == tenantId);

    public async Task<IEnumerable<Minister>> GetMinistersByTenantIdAsync(int tenantId)
        => await _dbContext.Set<Minister>()
                           .Include(m => m.Member)
                           .Include(m => m.Tenant)
                           .Where(m => m.TenantId == tenantId)
                           .ToListAsync();

    public async Task<DepartmentMembers?> GetDepartmentMemberAsync(int departmentId,
                                                                   int memberId,
                                                                   int tenantId)
        => await _dbContext.Set<DepartmentMembers>()
                           .Include(dm => dm.Member)
                           .Include(dm => dm.Department)
                           .SingleOrDefaultAsync(dm => dm.MemberId == memberId
                                                       && dm.DepartmentId == departmentId
                                                       && dm.Member.TenantId == tenantId
                                                       && dm.Department.TenantId == tenantId);

    public async Task<(int members, int newComers)> GetPersonsBetweenDatesByTenantIdAsync(int tenantId)
    {
        var members = await _dbContext.Set<Member>()
                                      .Where(f => f.TenantId == tenantId)
                                      .CountAsync();
            
        var newComers = await _dbContext.Set<NewComer>()
                                        .Where(f => f.TenantId == tenantId)
                                        .CountAsync();

        return (members, newComers);
    }
}