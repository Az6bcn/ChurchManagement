using Application.Interfaces.Repositories;
using Domain.Entities.PersonAggregate;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class DepartmentRepositoryAsync: GenericRepositoryAsync<Department>, IDepartmentReporsitory
    {
        public DepartmentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}