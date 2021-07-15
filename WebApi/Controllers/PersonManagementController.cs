using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PersonManagement.Create;
using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;
using Application.Dtos.Response.Get;
using Application.Helpers;
using Application.Queries.PersonManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonManagementController : ControllerBase
    {
        private readonly IQueryPersonManagement _personManagementQuery;
        private readonly ICreateDepartmentCommand _createDepartmentCommand;

        public PersonManagementController(IQueryPersonManagement personManagementQuery,
                                          ICreateDepartmentCommand createDepartmentCommand)
        {
            _personManagementQuery = personManagementQuery;
            _createDepartmentCommand = createDepartmentCommand;
        }

        /// <summary>
        /// Gets all departments for provided tenantId
        /// </summary>
        /// <param name="tenantId">The id of the tenant which you want to get its departments</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<GetDepartmentsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("departments/{tenantId:int}")]
        public async Task<IActionResult> GetDepartments(int tenantId)
        {
            if (tenantId <= 0)
                return BadRequest("Invalid tenantId");

            var queryResult =
                await _personManagementQuery.GetDepartmentsByTenantIdAsync(tenantId);

            if (!queryResult.Results.Any())
                return NotFound(ApiRequestResponse<GetDepartmentsResponseDto>
                                    .Fail($"No departments found for tenant {tenantId}"));

            return Ok(ApiRequestResponse<GetDepartmentsResponseDto>.Succeed(queryResult.Results.ToList()));
        }

        /// <summary>
        /// Creates a new department for the provided tenant
        /// </summary>
        /// <param name="tenantId">Id of tenant you're creating the department for. </param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<CreateDepartmentResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("departments/{tenantId}")]
        public async Task<IActionResult> CreateDepartment(int tenantId,
                                                          [FromBody] CreateDepartmentRequestDto request)
        {
            if (request is null || tenantId <= 0)
                BadRequest("Invalid request");

            if (request!.TenantId != tenantId)
                BadRequest("Invalid request");

            var department =
                await _createDepartmentCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<CreateDepartmentResponseDto>.Succeed(department));
        }
    }
}