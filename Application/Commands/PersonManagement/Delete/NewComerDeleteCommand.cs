using System;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagement;
using Domain.Entities.PersonAggregate;
using PersonManagementAggregate = Domain.Entities.PersonAggregate.PersonManagement;


namespace Application.Commands.PersonManagement.Delete
{
    public class NewComerDeleteCommand: IDeleteNewComerCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonManagementRepositoryAsync _personManagementRepo;
        private readonly IQueryPersonManagement _personManagementQuery;

        public NewComerDeleteCommand(IUnitOfWork unitOfWork,
                                     IPersonManagementRepositoryAsync personManagementRepo,
                                     IQueryPersonManagement personManagementQuery)
        {
            _unitOfWork = unitOfWork;
            _personManagementRepo = personManagementRepo;
            _personManagementQuery = personManagementQuery;
        }

        public async Task ExecuteAsync(int newComerId, int tenantId)
        {
            var newComer = await _personManagementQuery.GetNewComerByIdAsync(newComerId, tenantId);

            if (newComer is null)
                throw new ArgumentException($"Newcomer {newComer} not found ");

            PersonManagementAggregate.AssignNewComer(newComer);
            PersonManagementAggregate.DeleteNewComer();

            _personManagementRepo.Update<NewComer>(newComer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}