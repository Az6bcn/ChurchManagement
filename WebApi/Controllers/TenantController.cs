using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Tenant.Create;
using Application.Dtos;
using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;
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
        private readonly ICreateTenantCommand _createTenantCommand;

        public TenantController(IQueryTenantDetails queryTenantDetails,
                                ICreateTenantCommand createTenantCommand)
        {
            _queryTenantDetails = queryTenantDetails;
            _createTenantCommand = createTenantCommand;
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


        [ProducesResponseType(typeof(ApiRequestResponse<CreateTenantResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromBody] CreateTenantRequestDto request)
        {
            var response = await _createTenantCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<CreateTenantResponseDto>.Succeed(response));
        }
    }
}