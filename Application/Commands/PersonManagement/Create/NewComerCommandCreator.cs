using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagement;
using Application.Queries.Tenant;
using Application.RequestValidators;
using AutoMapper;
using Domain.Entities.PersonAggregate;
using Domain.ValueObjects;

namespace Application.Commands.PersonManagement.Create
{
    public class NewComerCommandCreator : ICreateNewComerCommand
    {
        private readonly IQueryTenant _tenantQuery;
        private readonly IQueryPersonManagement _personManagementQuery;
        private readonly IPersonManagementRepositoryAsync _personManagementRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatePersonManagementRequestDto _requestValidator;
        private readonly IMapper _mapper;

        public NewComerCommandCreator(IQueryTenant tenantQuery,
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

        public async Task<CreateNewComerResponseDto> ExecuteAsync(CreateNewComerRequestDto request)
        {
            var tenant = await _tenantQuery.GetTenantByIdAsync(request.TenantId);

            if (tenant is null)
                throw new ArgumentException("Invalid tenantId", nameof(request.TenantId));

            var person = Person.Create(request.TenantId,
                                       request.Name,
                                       request.Surname,
                                       request.DateAndMonthOfBirth,
                                       request.Gender,
                                       request.PhoneNumber);

            var personValidationErrors = person.Validate();
            if (personValidationErrors.Any())
                throw new RequestValidationException("Request failed validation",
                                                     new Dictionary<string, object>
                                                     {
                                                         {
                                                             "Request errors",
                                                             string.Join(" , ", personValidationErrors)
                                                         }
                                                     });

            Domain.Entities.PersonAggregate.PersonManagement.CreateNewComer(person,
                                                                             request.DateAttended,
                                                                             request.ServiceTypeEnum,
                                                                             tenant);
            
            var newComer = Domain.Entities.PersonAggregate.PersonManagement.NewComer;

            await _personManagementRepo.AddAsync<NewComer>(newComer);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CreateNewComerResponseDto>(newComer);
        }
    }
}