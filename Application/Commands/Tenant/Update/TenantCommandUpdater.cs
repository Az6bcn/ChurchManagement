using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Request.Update;
using Application.Dtos.Response.Update;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.Tenant;
using Application.RequestValidators;
using AutoMapper;
using TenantAggregate = Domain.Entities.TenantAggregate.Tenant;

namespace Application.Commands.Tenant.Update
{
    public class TenantCommandUpdater : IUpdateTenantCommand
    {
        private readonly IMapper _mapper;
        private readonly IValidateTenantRequestDto _requestValidator;
        private readonly IQueryTenant _tenantQuery;
        private readonly ITenantRepositoryAsync _tenantRepo;
        private readonly IUnitOfWork _uniOfWork;

        public TenantCommandUpdater(IMapper mapper,
                                    IValidateTenantRequestDto requestValidator,
                                    IQueryTenant queryTenant,
                                    ITenantRepositoryAsync tenantRepo,
                                    IUnitOfWork uniOfWork)
        {
            _mapper = mapper;
            _requestValidator = requestValidator;
            _tenantQuery = queryTenant;
            _tenantRepo = tenantRepo;
            _uniOfWork = uniOfWork;
        }

        public async Task<UpdateTenantResponseDto> ExecuteAsync(UpdateTenantRequestDto request)
        {
            var tenantNames = await _tenantQuery.GetTenantNamesAsync();

            _requestValidator.Validate(request, tenantNames.ToList(), out IDictionary<string, object> errors);

            if (errors.Any())
                throw new RequestValidationException("Request failed validation", errors);

            var tenant = await _tenantQuery.GetTenantByIdAsync(request.Id);

            if (tenant is null)
                throw new ArgumentException($"Tenant with id {request.Id} does not exist",
                                            nameof(request.Id));

            tenant.Update(request.Name, request.LogoUrl, request.CurrencyId);

            _tenantRepo.Update(tenant);
            await _uniOfWork.SaveChangesAsync();

            return _mapper.Map<UpdateTenantResponseDto>(tenant);
        }
    }
}