using Application.Interfaces.Repositories;
using Domain.Entities.DepartmentAggregate;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class DepartmentRepository: GenericRepository<Department>, IDepartmentReporsitory
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}