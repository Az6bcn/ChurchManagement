using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Tenant.Create;
using Application.Commands.Tenant.Delete;
using Application.Commands.Tenant.Update;
using Application.Dtos;
using Application.Dtos.Request.Create;
using Application.Dtos.Request.Update;
using Application.Dtos.Response.Create;
using Application.Dtos.Response.Get;
using Application.Dtos.Response.Update;
using Application.Interfaces.Repositories;
using Application.Queries.Tenant;
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
        private readonly IUpdateTenantCommand _updateTenantCommand;
        private readonly IDeleteTenantCommand _deleteTenantCommand;

        public TenantController(IQueryTenant tenantQuery,
                                IQueryTenantDetails queryTenantDetails,
                                ICreateTenantCommand createTenantCommand,
                                IUpdateTenantCommand updateTenantCommand,
                                IDeleteTenantCommand deleteTenantCommand)
        {
            _queryTenantDetails = queryTenantDetails;
            _createTenantCommand = createTenantCommand;
            _updateTenantCommand = updateTenantCommand;
            _deleteTenantCommand = deleteTenantCommand;
        }

        [ProducesResponseType(typeof(ApiRequestResponse<GetTenantResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{tenantId:int}")]
        public async Task<IActionResult> GetTenantDetails(int tenantId)
        {
            if (tenantId <= 0)
                return BadRequest("Invalid tenant");

            var response = await _queryTenantDetails.ExecuteAsync(tenantId);

            if (response?.Result is null)
                return NotFound(ApiRequestResponse<GetTenantResponseDto>.Fail($"Tenant {tenantId} found"));

            var result = 
                ApiRequestResponse<GetTenantResponseDto>.Succeed(result: response.Result);

            return Ok(result);
        }

        [ProducesResponseType(typeof(ApiRequestResponse<GetTenantResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{tenantGuidId:guid}")]
        public async Task<IActionResult> GetTenantDetails(Guid tenantGuidId)
        {
            if (tenantGuidId == Guid.Empty)
                return BadRequest();

            var response = await _queryTenantDetails.ExecuteAsync(tenantGuidId);

            if (response?.Result is null)
                return NotFound(ApiRequestResponse<GetTenantResponseDto>
                                    .Fail($"Tenant {tenantGuidId} found"));

            var result = 
                ApiRequestResponse<GetTenantResponseDto>.Succeed(result: response.Result);

            return Ok(result);
        }


        [ProducesResponseType(typeof(ApiRequestResponse<CreateTenantResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromBody] CreateTenantRequestDto request)
        {
            if (request is null)
                return BadRequest("Request cannot be empty");

            var response = await _createTenantCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<CreateTenantResponseDto>.Succeed(response));
        }


        [ProducesResponseType(typeof(ApiRequestResponse<UpdateTenantResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{tenantId:int}")]
        public async Task<IActionResult> UpdateTenant([FromBody] UpdateTenantRequestDto request,
                                                      int tenantId)
        {
            if (request is null)
                return BadRequest("Request cannot be empty");

            if (request.TenantId != tenantId)
                return BadRequest("Invalid request");

            var response = await _updateTenantCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<UpdateTenantResponseDto>.Succeed(response));
        }


        [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{tenantId:int}")]
        public async Task<IActionResult> DeleteTenant(int tenantId)
        {
            if (tenantId <= 0)
                return BadRequest();

            await _deleteTenantCommand.ExecuteAsync(tenantId);

            return Ok(ApiRequestResponse<string>.Succeed($"{tenantId} Deleted successfully"));
        }
    }
}