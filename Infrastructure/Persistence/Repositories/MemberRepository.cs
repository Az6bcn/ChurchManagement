using Application.Interfaces.Repositories;
using Domain.Entities.PersonAggregate;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class MemberRepository: GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}