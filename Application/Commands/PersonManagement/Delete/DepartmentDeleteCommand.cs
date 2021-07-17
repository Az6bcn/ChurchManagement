using System;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagement;
using Application.RequestValidators;
using PersonManagementAggregate = Domain.Entities.PersonAggregate.PersonManagement;

namespace Application.Commands.PersonManagement.Delete
{
    public class DepartmentDeleteCommand: IDeleteDepartmentCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonManagementRepositoryAsync _personManagementRepo;
        private readonly IQueryPersonManagement _personManagementQuery;
        private readonly IValidatePersonManagementRequestDto _requestValidator;

        public DepartmentDeleteCommand(IUnitOfWork unitOfWork,
                                       IPersonManagementRepositoryAsync personManagementRepo,
                                       IQueryPersonManagement personManagementQuery,
                                       IValidatePersonManagementRequestDto requestValidator)
        {
            _unitOfWork = unitOfWork;
            _personManagementRepo = personManagementRepo;
            _personManagementQuery = personManagementQuery;
            _requestValidator = requestValidator;
        }

        public async Task ExecuteAsync(int departmentId, int tenantId)
        {
            var department = await _personManagementQuery.GetDepartmentIdAsync(departmentId, tenantId);

            if (department is null)
                throw new ArgumentException($"Department {department} not found ");

            PersonManagementAggregate.AssignDepartment(department);
            PersonManagementAggregate.DeleteDepartment();

            _personManagementRepo.Update(department);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}