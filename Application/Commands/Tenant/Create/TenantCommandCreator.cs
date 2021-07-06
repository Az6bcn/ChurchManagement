using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.Tenant;
using Application.RequestValidators;
using AutoMapper;
using Domain.Validators;
using TenantAggregate = Domain.Entities.TenantAggregate.Tenant;

namespace Application.Commands.Tenant.Create
{
    public class TenantCommandCreator : ICreateTenantCommand
    {
        private readonly ITenantRepositoryAsync _tenantRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidateTenantRequestDto _requestValidator;
        private readonly IQueryTenant _tenantQuery;
        private readonly IValidateTenantCreation _validateTenantCreation;

        public TenantCommandCreator(ITenantRepositoryAsync tenantRepo,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper,
                                    IValidateTenantRequestDto requestValidator,
                                    IQueryTenant tenantQuery,
                                    IValidateTenantCreation validateTenantCreation)
        {
            _tenantRepo = tenantRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestValidator = requestValidator;
            _tenantQuery = tenantQuery;
            _validateTenantCreation = validateTenantCreation;
        }
        
        public async Task<CreateTenantResponseDto> ExecuteAsync(CreateTenantRequestDto request)
        {
            var tenants = await _tenantRepo.GetAllAsync();
            var tenantNames = tenants.Select(x => x.Name).ToList();

            _requestValidator.Validate(request, tenantNames, out IDictionary<string, object> errors);
            if (errors.Any())
                throw new RequestValidationException("Request failed validation", errors);

            var tenant = TenantAggregate.Create(request.Name,
                                                request.LogoUrl ?? string.Empty,
                                                request.CurrencyId,
                                                _validateTenantCreation,
                                                out var domainErrors);

            if (domainErrors.Any())
                throw new DomainValidationException("Failed domain validation", domainErrors);

            await _tenantRepo.AddAsync(tenant);
            await _unitOfWork.SaveChangesAsync();

            var mappedResponse = _mapper.Map<CreateTenantResponseDto>(tenant);

            return mappedResponse;
        }
    }
}