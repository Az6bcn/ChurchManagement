using Application.Interfaces.Repositories;
using Domain.Entities.FinanceAggregate;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class FinanceRepositoryAsync : GenericRepositoryAsync<Finance>, IFinanceRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public FinanceRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}