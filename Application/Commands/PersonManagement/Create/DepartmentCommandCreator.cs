using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagement;
using Application.Queries.Tenant;
using Application.RequestValidators;
using Domain.Entities.PersonAggregate;

namespace Application.Commands.PersonManagement.Create
{
    public class DepartmentCommandCreator : ICreateDepartmentCommand
    {
        private readonly IQueryPersonManagement _personManagementQuery;
        private readonly IQueryTenant _tenantQuery;
        private readonly IPersonManagementRepositoryAsync _personManagementRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatePersonManagementRequestDto _requestValidator;

        public DepartmentCommandCreator(IQueryPersonManagement personManagementQuery,
                                        IQueryTenant tenantQuery,
                                        IPersonManagementRepositoryAsync personManagementRepo,
                                        IUnitOfWork unitOfWork,
                                        IValidatePersonManagementRequestDto requestValidator)
        {
            _personManagementQuery = personManagementQuery;
            _tenantQuery = tenantQuery;
            _personManagementRepo = personManagementRepo;
            _unitOfWork = unitOfWork;
            _requestValidator = requestValidator;
        }

        public async Task<CreateDepartmentResponseDto> ExecuteAsync(CreateDepartmentRequestDto request)
        {
            var departmentNames = await _personManagementQuery
                                      .GetDepartmentNamesByTenantIdAsync(request.TenantId);

            var tenant = await _tenantQuery.GetTenantByIdAsync(request.TenantId);
            
            _requestValidator.ValidateDepartment(request, departmentNames.ToList(), out var errors);

            if (errors.Any())
                throw new RequestValidationException("Failed validation", errors);

            var department = Department.Create(request.Name, tenant);

            await _personManagementRepo.AddAsync<Department>(department);

            await _unitOfWork.SaveChangesAsync();

            return null;
        }
    }
}