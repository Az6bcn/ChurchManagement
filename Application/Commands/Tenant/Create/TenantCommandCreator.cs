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

        public TenantCommandCreator(ITenantRepositoryAsync tenantRepo,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper,
                                    IValidateTenantRequestDto requestValidator,
                                    IQueryTenant tenantQuery)
        {
            _tenantRepo = tenantRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestValidator = requestValidator;
            _tenantQuery = tenantQuery;
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
                                                request.TenantStatusId);

            await _tenantRepo.AddAsync(tenant);
            await _unitOfWork.SaveChangesAsync();

            var mappedResponse = _mapper.Map<CreateTenantResponseDto>(tenant);

            return mappedResponse;
        }
    }
}