using System;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Helpers;
using Application.Interfaces.Repositories;
using AutoMapper;

namespace Application.Queries.Tenant.TenantDetails
{
    public class TenantDetailsQuery: IQueryTenantDetails
    {
        private readonly ITenantRepositoryAsync _tenantRepo;
        private readonly IMapper _mapper;

        public TenantDetailsQuery(ITenantRepositoryAsync tenantRepo, IMapper mapper)
        {
            _tenantRepo = tenantRepo;
            _mapper = mapper;
        }

        public async Task<QueryResult<TenantDetailsDto?>> ExecuteAsync (Guid tenantGuidId)
        {
            var response = await _tenantRepo.GetTenantByGuidIdAsync(tenantGuidId);

            if(response is null)
                return default;
            
            var tenantDetails = _mapper.Map<TenantDetailsDto>(response);

            return QueryResult<TenantDetailsDto>.CreateQueryResult(tenantDetails);
        }
        
        
    }
}