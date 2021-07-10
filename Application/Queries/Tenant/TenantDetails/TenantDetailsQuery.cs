using System;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Response.Get;
using Application.Helpers;
using Application.Interfaces.Repositories;
using AutoMapper;

namespace Application.Queries.Tenant.TenantDetails
{
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

        public async Task<QueryResult<GetTenantResponseDto?>> ExecuteAsync (Guid tenantGuidId)
        {
            var response = await _tenantQuery.GetTenantByGuidIdAsync(tenantGuidId);

            if(response is null)
                return default;
            
            var tenantDetails = _mapper.Map<GetTenantResponseDto>(response);

            return QueryResult<GetTenantResponseDto>.CreateQueryResult(tenantDetails);
        }

        public async Task<QueryResult<GetTenantResponseDto?>> ExecuteAsync(int tenantId)
        {
            var response = await _tenantQuery.GetTenantByIdAsync(tenantId);

            if(response is null)
                return default;
            
            var tenantDetails = _mapper.Map<GetTenantResponseDto>(response);

            return QueryResult<GetTenantResponseDto>.CreateQueryResult(tenantDetails);
        }
    }
}