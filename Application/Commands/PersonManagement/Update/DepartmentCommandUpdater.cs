using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Request.Update;
using Application.Dtos.Response.Create;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagement;
using Application.Queries.Tenant;
using Application.RequestValidators;
using AutoMapper;
using Domain.Entities.PersonAggregate;

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

        public async Task<dynamic> ExecuteAsync(UpdateDepartmentRequestDto request)
        {
            var departments = await _personManagementQuery
                                  .GetTenantDepartmentsByTenantIdAsync(request.TenantId);
            
            if (!departments.Any())
                throw new InvalidOperationException($"No departments for the tenant {request.TenantId} to update");

            var tenant = await _tenantQuery.GetTenantByIdAsync(request.TenantId);

            if (tenant is null)
                throw new ArgumentException("Invalid tenantId", nameof(request.TenantId));

            _requestValidator.ValidateDepartmentUpdate(request, departments.ToList(), out var errors);

            if (errors.Any())
                throw new RequestValidationException("Failed validation", errors);

            var department = Department.Create(request.Name, tenant);

            await _personManagementRepo.AddAsync<Department>(department);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CreateDepartmentResponseDto>(department);
        }
    }
}