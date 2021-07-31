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
using PersonManagementAggregate = Domain.Entities.PersonAggregate.PersonManagement;

namespace Application.Commands.PersonManagement.Create
{
    public class MinisterCommandCreator: ICreateMinisterCommand
    {
        private readonly IQueryTenant _tenantQuery;
        private readonly IQueryPersonManagement _personManagementQuery;
        private readonly IPersonManagementRepositoryAsync _personManagementRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatePersonManagementRequestDto _requestValidator;
        private readonly IMapper _mapper;

        public MinisterCommandCreator(IQueryTenant tenantQuery,
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
        

        public async Task<CreateMinisterResponseDto> ExecuteAsync(CreateMinisterRequestDto request)
        {
            var member = await _personManagementQuery.GetMemberByIdAsync(request.MemberId, request.TenantId);

            if (member is null)
                throw new ArgumentException("Invalid memberId", nameof(request.MemberId));
            
            PersonManagementAggregate.CreateMinister(member, request.MinisterTitle);
            var minister = PersonManagementAggregate.Minister;

            await _personManagementRepo.AddAsync<Minister>(minister);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CreateMinisterResponseDto>(minister);
        }
    }
}