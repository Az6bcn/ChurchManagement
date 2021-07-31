using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Application.Dtos.Request.Update;
using Application.Dtos.Response.Create;
using Application.Dtos.Response.Update;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.PersonManagement;
using Application.Queries.Tenant;
using Application.RequestValidators;
using AutoMapper;
using Domain.Entities.PersonAggregate;
using Domain.ValueObjects;
using PersonManagementAggregate = Domain.Entities.PersonAggregate.PersonManagement;


namespace Application.Commands.PersonManagement.Update
{
    public class MemberCommandUpdater : IUpdateMemberCommand
    {
        private readonly IQueryTenant _tenantQuery;
        private readonly IQueryPersonManagement _personManagementQuery;
        private readonly IPersonManagementRepositoryAsync _personManagementRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatePersonManagementRequestDto _requestValidator;
        private readonly IMapper _mapper;

        public MemberCommandUpdater(IQueryTenant tenantQuery,
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

        public async Task<UpdateMemberResponseDto> ExecuteAsync(UpdateMemberRequestDto request)
        {
            var member
                = await _personManagementQuery.GetMemberByIdAsync(request.MemberId,
                                                                  request.TenantId);

            if (member is null)
                throw new InvalidOperationException($"member {request.MemberId} not found");

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
                                                             string.Join(" , ",
                                                                         personValidationErrors)
                                                         }
                                                     });

            PersonManagementAggregate.AssignMember(member);
            PersonManagementAggregate.UpdateMember(person, request.IsWorker);

            _personManagementRepo.Update<Member>(PersonManagementAggregate.Member);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UpdateMemberResponseDto>(PersonManagementAggregate.Member);
        }
    }
}