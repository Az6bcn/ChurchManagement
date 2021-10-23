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
    public class MinistersController : ControllerBase
    {
        private readonly IQueryPersonManagement _personManagementQuery;
        private readonly ICreateMinisterCommand _createMinisterCommand;
        private readonly IUpdateMinisterCommand _updateMinisterCommand;
        private readonly IDeleteMinisterCommand _deleteMinisterCommand;

        public MinistersController(IQueryPersonManagement personManagementQuery,
                                   ICreateMinisterCommand createMinisterCommand,
                                   IUpdateMinisterCommand updateMinisterCommand,
                                   IDeleteMinisterCommand deleteMinisterCommand)
        {
            _personManagementQuery = personManagementQuery;
            _createMinisterCommand = createMinisterCommand;
            _updateMinisterCommand = updateMinisterCommand;
            _deleteMinisterCommand = deleteMinisterCommand;
        }

        /// <summary>
        /// Gets all ministers for provided tenantId
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<GetMinistersResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet()]
        public async Task<IActionResult> GetMinisters()
        {
            var tenantId = HttpContext.GetTenantId();
            
            if (tenantId <= 0)
                return BadRequest("Invalid tenantId");

            var queryResult =
                await _personManagementQuery.GetMinistersByTenantIdAsync(tenantId);

            if (!queryResult.Results.Any())
                return NotFound(ApiRequestResponse<GetMinistersResponseDto>
                                    .Fail($"No ministers found for tenant {tenantId}"));

            return Ok(ApiRequestResponse<GetMinistersResponseDto>.Succeed(queryResult.Results.ToList()));
        }

        /// <summary>
        /// Creates a new minister for the provided tenant
        /// </summary>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<CreateMinisterResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost()]
        public async Task<IActionResult> CreateMinisters([FromBody] CreateMinisterRequestDto request)
        {
            var tenantId = HttpContext.GetTenantId();
            
            if (request is null || tenantId <= 0)
                return BadRequest("Invalid request");

            if (request!.TenantId != tenantId)
                return BadRequest("Invalid request");

            var minister =
                await _createMinisterCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<CreateMinisterResponseDto>.Succeed(minister));
        }

        /// <summary>
        /// Updates a minister for the provided tenant
        /// </summary>
        /// <param name="ministerId">Id of the minister to update for the tenant</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<UpdateMinisterResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{ministerId:int}")]
        public async Task<IActionResult> UpdateMinister(int ministerId,
                                                      [FromBody] UpdateMinisterRequestDto request)
        {
            var tenantId = HttpContext.GetTenantId();

            if (request is null || tenantId <= 0 || request.MinisterId <= 0)
                return BadRequest("Invalid request");

            if (request!.TenantId != tenantId || request.MinisterId != ministerId)
                return BadRequest("Invalid request");

            var minister = await _updateMinisterCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<UpdateMinisterResponseDto>.Succeed(minister));
        }

        /// <summary>
        /// Deletes a minister for the provided tenant
        /// </summary>
        /// <param name="ministerId">Id the of the minister to delete for the tenant</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{ministerId:int}")]
        public async Task<IActionResult> DeleteMinister(int ministerId)
        {
            var tenantId = HttpContext.GetTenantId();

            if (tenantId <= 0 || ministerId <= 0)
                BadRequest("Invalid request");

            await _deleteMinisterCommand.ExecuteAsync(ministerId, tenantId);

            return Ok(ApiRequestResponse<string>.Succeed("Deleted successfully"));
        }
    }
}