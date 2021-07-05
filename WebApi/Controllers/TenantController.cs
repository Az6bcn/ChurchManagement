using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Queries.Tenant.TenantDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly IQueryTenantDetails _queryTenantDetails;

        public TenantController(IQueryTenantDetails queryTenantDetails)
        {
            _queryTenantDetails = queryTenantDetails;
        }
        
        [ProducesResponseType(typeof(ApiRequestResponse<TenantDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{tenantGuidId}")]
        public async Task<IActionResult> GetTenantDetails(Guid tenantGuidId)
        {
            if (tenantGuidId == Guid.Empty)
                return BadRequest();
                
            var response = await _queryTenantDetails.ExecuteAsync(tenantGuidId);

            if (response.Result is null)
                return NotFound(ApiRequestResponse<TenantDetailsDto>.Fail("Tenant details not found"));

            var result = ApiRequestResponse<TenantDetailsDto>.Succeed(result: response.Result);
            
            return Ok(result);
        }
    }
}