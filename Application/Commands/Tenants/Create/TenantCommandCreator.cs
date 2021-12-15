using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.Tenants;
using Application.RequestValidators;
using AutoMapper;
using Domain.Validators;
using TenantAggregate = Domain.Entities.TenantAggregate.Tenant;

namespace Application.Commands.Tenants.Create;

public class TenantCommandCreator : ICreateTenantCommand
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IQueryTenant _tenantQuery;
    private readonly ITenantRepositoryAsync _tenantRepo;
    private readonly IValidateTenantInDomain _domainValidator;
    private readonly IValidateTenantRequestDto _requestValidator;

    public TenantCommandCreator(ITenantRepositoryAsync tenantRepo,
                                IUnitOfWork unitOfWork,
                                IMapper mapper,
                                IValidateTenantRequestDto requestValidator,
                                IQueryTenant tenantQuery,
                                IValidateTenantInDomain domainValidator)
    {
        _tenantRepo = tenantRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _requestValidator = requestValidator;
        _tenantQuery = tenantQuery;
        _domainValidator = domainValidator;
    }
        
    public async Task<CreateTenantResponseDto> ExecuteAsync(CreateTenantRequestDto request)
    {
        var tenantNames = await _tenantQuery.GetTenantNamesAsync();

        _requestValidator.Validate(request, tenantNames.ToList(), out var errors);
        if (errors.Any())
            throw new RequestValidationException("Request failed validation", errors);

        var tenant = TenantAggregate.Create(request.Name,
                                            request.LogoUrl ?? string.Empty,
                                            request.CurrencyId,
                                            _domainValidator,
                                            out var domainErrors);

        if (domainErrors.Any())
            throw new DomainValidationException("Request failed domain validation", domainErrors);

        await _tenantRepo.AddAsync(tenant);
        await _unitOfWork.SaveChangesAsync();

        var mappedResponse = _mapper.Map<CreateTenantResponseDto>(tenant);

        return mappedResponse;
    }
}