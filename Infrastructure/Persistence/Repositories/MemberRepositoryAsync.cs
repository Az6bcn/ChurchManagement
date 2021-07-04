using Application.Interfaces.Repositories;
using Domain.Entities.PersonAggregate;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class MemberRepositoryAsync: GenericRepositoryAsync<Member>, IMemberRepositoryAsync
    {
        public MemberRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}