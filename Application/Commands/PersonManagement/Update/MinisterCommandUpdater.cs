using System;
using System.Collections.Generic;
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
using Domain.ValueObjects;
using PersonManagementAggregate = Domain.Entities.PersonAggregate.PersonManagement;

namespace Application.Commands.PersonManagement.Update
{
    public class MinisterCommandUpdater: IUpdateMinisterCommand
    {
        private readonly IQueryTenant _tenantQuery;
        private readonly IQueryPersonManagement _personManagementQuery;
        private readonly IPersonManagementRepositoryAsync _personManagementRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatePersonManagementRequestDto _requestValidator;
        private readonly IMapper _mapper;

        public MinisterCommandUpdater(IQueryTenant tenantQuery,
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

        public async Task<UpdateMinisterResponseDto> ExecuteAsync(UpdateMinisterRequestDto request)
        {
            var minister
                = await _personManagementQuery.GetMinisterByIdAsync(request.MinisterId,
                                                                    request.TenantId);

            if (minister is null)
                throw new InvalidOperationException($"Minister {request.MinisterId} not found");

            PersonManagementAggregate.AssignMinister(minister);
            PersonManagementAggregate.UpdateMinister(minister, request.MinisterTitle);

            _personManagementRepo.Update<Minister>(PersonManagementAggregate.Minister);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UpdateMinisterResponseDto>(PersonManagementAggregate.Member);
        }
    }
}