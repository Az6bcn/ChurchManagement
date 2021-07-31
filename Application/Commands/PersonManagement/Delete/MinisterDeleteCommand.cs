using System;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagement;
using PersonManagementAggregate = Domain.Entities.PersonAggregate.PersonManagement;

namespace Application.Commands.PersonManagement.Delete
{
    public class MinisterDeleteCommand: IDeleteMinisterCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonManagementRepositoryAsync _personManagementRepo;
        private readonly IQueryPersonManagement _personManagementQuery;

        public MinisterDeleteCommand(IUnitOfWork unitOfWork,
                                     IPersonManagementRepositoryAsync personManagementRepo,
                                     IQueryPersonManagement personManagementQuery)
        {
            _unitOfWork = unitOfWork;
            _personManagementRepo = personManagementRepo;
            _personManagementQuery = personManagementQuery;
        }

        public async Task ExecuteAsync(int memberId, int tenantId)
        {
            var minister = await _personManagementQuery.GetMinisterByIdAsync(memberId, tenantId);

            if (minister is null)
                throw new ArgumentException($"Minister {memberId} not found ");

            PersonManagementAggregate.AssignMinister(minister);
            PersonManagementAggregate.DeleteMember();

            _personManagementRepo.Update(minister);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}