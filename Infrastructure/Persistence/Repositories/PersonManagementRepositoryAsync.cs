using Application.Interfaces.Repositories;
using Domain.Entities.PersonAggregate;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class PersonalManagementRepositoryAsync: GenericRepositoryAsync<Department>, IPersonalManagementRepositoryAsync
    {
        public PersonalManagementRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}