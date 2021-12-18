using Application.Dtos.Response.Get;
using Application.Helpers;
using AutoMapper;

namespace Application.Queries.Tenants.TenantDetails;

public class TenantDetailsQuery: IQueryTenantDetails
{
    private readonly IQueryTenant _tenantQuery;
    private readonly IMapper _mapper;

    public TenantDetailsQuery(IQueryTenant tenantQuery,
                              IMapper mapper)
    {
        _tenantQuery = tenantQuery;
        _mapper = mapper;
    }

    public async Task<QueryResult<GetTenantsResponseDto?>> ExecuteAsync (Guid tenantGuidId)
    {
        var response = await _tenantQuery.GetTenantByGuidIdAsync(tenantGuidId);

        if(response is null)
            return default;
            
        var tenantDetails = _mapper.Map<GetTenantsResponseDto>(response);

        return QueryResult<GetTenantsResponseDto>.CreateQueryResult(tenantDetails);
    }

    public async Task<QueryResult<GetTenantsResponseDto?>> ExecuteAsync(int tenantId)
    {
        var response = await _tenantQuery.GetTenantByIdAsync(tenantId);

        if(response is null)
            return default;
            
        var tenantDetails = _mapper.Map<GetTenantsResponseDto>(response);

        return QueryResult<GetTenantsResponseDto>.CreateQueryResult(tenantDetails);
    }
}