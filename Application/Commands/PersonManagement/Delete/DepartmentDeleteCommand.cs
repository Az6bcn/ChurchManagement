using System;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagement;
using PersonManagementAggregate = Domain.Entities.PersonAggregate.PersonManagement;

namespace Application.Commands.PersonManagement.Delete
{
    public class DepartmentDeleteCommand: IDeleteDepartmentCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonManagementRepositoryAsync _personManagementRepo;
        private readonly IQueryPersonManagement _personManagementQuery;

        public DepartmentDeleteCommand(IUnitOfWork unitOfWork,
                                       IPersonManagementRepositoryAsync personManagementRepo,
                                       IQueryPersonManagement personManagementQuery)
        {
            _unitOfWork = unitOfWork;
            _personManagementRepo = personManagementRepo;
            _personManagementQuery = personManagementQuery;
        }

        public async Task ExecuteAsync(int departmentId, int tenantId)
        {
            var department = await _personManagementQuery.GetDepartmentIdAsync(departmentId, tenantId);

            if (department is null)
                throw new ArgumentException($"Department {departmentId} not found ");

            PersonManagementAggregate.AssignDepartment(department);
            PersonManagementAggregate.DeleteDepartment();

            _personManagementRepo.Update(department);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}