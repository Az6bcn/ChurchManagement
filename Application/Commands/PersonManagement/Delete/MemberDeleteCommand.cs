using System;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagement;
using Application.RequestValidators;
using PersonManagementAggregate = Domain.Entities.PersonAggregate.PersonManagement;


namespace Application.Commands.PersonManagement.Delete
{
    public class MemberDeleteCommand: IDeleteMemberCommand
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonManagementRepositoryAsync _personManagementRepo;
        private readonly IQueryPersonManagement _personManagementQuery;

        public MemberDeleteCommand(IUnitOfWork unitOfWork,
                                   IPersonManagementRepositoryAsync personManagementRepo,
                                   IQueryPersonManagement personManagementQuery)
        {
            _unitOfWork = unitOfWork;
            _personManagementRepo = personManagementRepo;
            _personManagementQuery = personManagementQuery;
        }

        public async Task ExecuteAsync(int memberId, int tenantId)
        {
            var member = await _personManagementQuery.GetMemberByIdAsync(memberId, tenantId);

            if (member is null)
                throw new ArgumentException($"Member {memberId} not found ");

            PersonManagementAggregate.AssignMember(member);
            PersonManagementAggregate.DeleteMember();

            _personManagementRepo.Update(member);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}