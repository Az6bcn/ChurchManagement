using Application.Dtos.Request.Update;
using Application.Dtos.Response.Update;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.Tenants;
using Application.RequestValidators;
using AutoMapper;
using Domain.Validators;

namespace Application.Commands.Tenants.Update;

public class TenantCommandUpdater : IUpdateTenantCommand
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uniOfWork;
    private readonly IQueryTenant _tenantQuery;
    private readonly ITenantRepositoryAsync _tenantRepo;
    private readonly IValidateTenantInDomain _domainValidator;
    private readonly IValidateTenantRequestDto _requestValidator;

    public TenantCommandUpdater(IMapper mapper,
                                IQueryTenant tenantQuery,
                                ITenantRepositoryAsync tenantRepo,
                                IUnitOfWork uniOfWork,
                                IValidateTenantRequestDto requestValidator,
                                IValidateTenantInDomain domainValidator)
    {
        _mapper = mapper;
        _tenantQuery = tenantQuery;
        _tenantRepo = tenantRepo;
        _uniOfWork = uniOfWork;
        _requestValidator = requestValidator;
        _domainValidator = domainValidator;
    }

    public async Task<UpdateTenantResponseDto> ExecuteAsync(UpdateTenantRequestDto request)
    {
        var tenantNames = await _tenantQuery.GetTenantNamesAsync();

        _requestValidator.Validate(request, tenantNames.ToList(), out var errors);

        if (errors.Any())
            throw new RequestValidationException("Request failed validation", errors);

        var tenant = await _tenantQuery.GetTenantByIdAsync(request.TenantId);

        if (tenant is null)
            throw new ArgumentException($"Tenant {request.TenantId} not found",
                                        nameof(request.TenantId));

        tenant.Update(request.Name,
                      request.LogoUrl,
                      request.CurrencyId,
                      request.TenantStatus,
                      _domainValidator,
                      out var domainErrors);


        if (domainErrors.Any())
            throw new DomainValidationException("Request failed domain validation",
                                                domainErrors);

        _tenantRepo.Update(tenant);
        await _uniOfWork.SaveChangesAsync();

        return _mapper.Map<UpdateTenantResponseDto>(tenant);
    }
}