using System;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ITenantRepositoryAsync Tenants => new TenantRepositoryAsync(_dbContext);
        public IMemberRepositoryAsync Members => new MemberRepositoryAsync(_dbContext);
        public IDepartmentReporsitory Departments => new DepartmentRepositoryAsync(_dbContext);
        
        
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
