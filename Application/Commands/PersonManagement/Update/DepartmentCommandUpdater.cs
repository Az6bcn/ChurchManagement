using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Request.Update;
using Application.Dtos.Response.Update;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagement;
using Application.Queries.Tenant;
using Application.RequestValidators;
using AutoMapper;
using Domain.Entities.PersonAggregate;
using PersonManagementAggregate = Domain.Entities.PersonAggregate.PersonManagement;


namespace Application.Commands.PersonManagement.Update
{
    public class DepartmentCommandUpdater : IUpdateDepartmentCommand
    {
        private readonly IQueryTenant _tenantQuery;
        private readonly IQueryPersonManagement _personManagementQuery;
        private readonly IPersonManagementRepositoryAsync _personManagementRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatePersonManagementRequestDto _requestValidator;
        private readonly IMapper _mapper;

        public DepartmentCommandUpdater(IQueryTenant tenantQuery,
                                        IQueryPersonManagement personManagementQuery,
                                        IPersonManagementRepositoryAsync personManagementRepo,
                                        IUnitOfWork unitOfWork,
                                        IValidatePersonManagementRequestDto requestValidator,
                                        IMapper mapper)
        {
            _tenantQuery = tenantQuery;
            _personManagementQuery = personManagementQuery;
            _personManagementRepo = personManagementRepo;
            _unitOfWork = unitOfWork;
            _requestValidator = requestValidator;
            _mapper = mapper;
        }

        public async Task<UpdateDepartmentResponseDto> ExecuteAsync(UpdateDepartmentRequestDto request)
        {
            var departments = await _personManagementQuery
                                  .GetTenantDepartmentsByTenantIdAsync(request.TenantId);
            
            if (departments is null || !departments.Any())
                throw new InvalidOperationException($"No departments for the tenant {request.TenantId} to update");

            _requestValidator.ValidateDepartmentUpdate(request, departments.ToList(), out var errors);

            if (errors.Any())
                throw new RequestValidationException("Request failed validation", errors);

            var department = departments.Single(x => x.DepartmentId == request.DepartmentId);
            PersonManagementAggregate.AssignDepartment(department);
            PersonManagementAggregate.UpdateDepartment(request.Name);

            _personManagementRepo.Update<Department>(department);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UpdateDepartmentResponseDto>(department);
        }
    }
}