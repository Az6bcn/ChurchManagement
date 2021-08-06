using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PersonManagement.Create;
using Application.Commands.PersonManagement.Delete;
using Application.Commands.PersonManagement.Update;
using Application.Dtos.Request.Create;
using Application.Dtos.Request.Update;
using Application.Dtos.Response.Create;
using Application.Dtos.Response.Get;
using Application.Dtos.Response.Update;
using Application.Queries.PersonManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewComersController : ControllerBase
    {
        private readonly IQueryPersonManagement _personManagementQuery;
        private readonly ICreateNewComerCommand _createNewComerCommand;
        private readonly IUpdateNewComerCommand _updateNewComerCommand;
        private readonly IDeleteNewComerCommand _deleteNewComerCommand;

        public NewComersController(IQueryPersonManagement personManagementQuery,
                                   ICreateNewComerCommand createNewComerCommand,
                                   IUpdateNewComerCommand updateNewComerCommand,
                                   IDeleteNewComerCommand deleteNewComerCommand)
        {
            _personManagementQuery = personManagementQuery;
            _createNewComerCommand = createNewComerCommand;
            _updateNewComerCommand = updateNewComerCommand;
            _deleteNewComerCommand = deleteNewComerCommand;
        }

        /// <summary>
        /// Gets all newcomers for provided tenantId
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<GetMembersResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet()]
        public async Task<IActionResult> GetNewcomers()
        {
            var tenantId = 0;
            if (tenantId <= 0)
                return BadRequest("Invalid tenantId");

            var queryResult =
                await _personManagementQuery.GetNewComersByTenantIdAsync(tenantId);

            if (!queryResult.Results.Any())
                return NotFound(ApiRequestResponse<GetMembersResponseDto>
                                    .Fail($"No newcomers found for tenant {tenantId}"));

            return Ok(ApiRequestResponse<GetNewComersResponseDto>.Succeed(queryResult.Results.ToList()));
        }

        /// <summary>
        /// Creates a new newcomer for the provided tenant
        /// </summary>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<CreateNewComerResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost()]
        public async Task<IActionResult> CreateNewComer([FromBody] CreateNewComerRequestDto request)
        {
            var tenantId = 0;
            if (request is null || tenantId <= 0)
                return BadRequest("Invalid request");

            if (request!.TenantId != tenantId)
                return BadRequest("Invalid request");

            var newComer =
                await _createNewComerCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<CreateNewComerResponseDto>.Succeed(newComer));
        }

        /// <summary>
        /// Updates a newcomer for the provided tenant
        /// </summary>
        /// <param name="newcomerId">Id of the newcomer to update for the tenant</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<UpdateNewComerResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{newcomerId:int}")]
        public async Task<IActionResult> UpdateNewComer(int newcomerId,
                                                      [FromBody] UpdateNewComerRequestDto request)
        {
            var tenantId = 0;
            if (request is null || tenantId <= 0 || request.NewComerId <= 0)
                return BadRequest("Invalid request");

            if (request!.TenantId != tenantId || request.NewComerId != newcomerId)
                return BadRequest("Invalid request");

            var newComer = await _updateNewComerCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<UpdateNewComerResponseDto>.Succeed(newComer));
        }

        /// <summary>
        /// Deletes a newcomer for the provided tenant
        /// </summary>
        /// <param name="newcomerId">Id the of the newcomer to delete for the tenant</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{newcomerId:int}")]
        public async Task<IActionResult> DeleteNewComer(int newcomerId)
        {
            var tenantId = 0;
            if (tenantId <= 0 || newcomerId <= 0)
                BadRequest("Invalid request");

            await _deleteNewComerCommand.ExecuteAsync(newcomerId, tenantId);

            return Ok(ApiRequestResponse<string>.Succeed("Deleted successfully"));
        }
    }
}