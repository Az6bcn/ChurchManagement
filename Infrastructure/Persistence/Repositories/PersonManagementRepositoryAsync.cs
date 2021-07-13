using Application.Interfaces.Repositories;
using Domain.Entities.PersonAggregate;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class PersonManagementRepositoryAsync: IPersonManagementRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        public PersonManagementRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}