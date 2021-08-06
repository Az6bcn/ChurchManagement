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
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IQueryPersonManagement _personManagementQuery;
        
        private readonly ICreateMemberCommand _createMemberCommand;
        private readonly IUpdateMemberCommand _updateMemberCommand;
        private readonly IDeleteMemberCommand _deleteMemberCommand;
        
        public MembersController(IQueryPersonManagement personManagementQuery,
                                          ICreateMemberCommand createMemberCommand,
                                          IUpdateMemberCommand updateMemberCommand,
                                          IDeleteMemberCommand deleteMemberCommand)
        {
            _personManagementQuery = personManagementQuery;
            _createMemberCommand = createMemberCommand;
            _updateMemberCommand = updateMemberCommand;
            _deleteMemberCommand = deleteMemberCommand;
        }

        /// <summary>
        /// Gets all members for provided tenantId
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<GetMembersResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet()]
        public async Task<IActionResult> GetMembers()
        {
            var tenantId = 0;
            if (tenantId <= 0)
                return BadRequest("Invalid tenantId");

            var queryResult =
                await _personManagementQuery.GetMembersByTenantIdAsync(tenantId);

            if (!queryResult.Results.Any())
                return NotFound(ApiRequestResponse<GetMembersResponseDto>
                                    .Fail($"No members found for tenant {tenantId}"));

            return Ok(ApiRequestResponse<GetMembersResponseDto>.Succeed(queryResult.Results.ToList()));
        }

        /// <summary>
        /// Creates a new Member for the provided tenant
        /// </summary>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<CreateMemberResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost()]
        public async Task<IActionResult> CreateMember([FromBody] CreateMemberRequestDto request)
        {
            var tenantId = 0;
            if (request is null || tenantId <= 0)
                return BadRequest("Invalid request");

            if (request!.TenantId != tenantId)
                return BadRequest("Invalid request");

            var member =
                await _createMemberCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<CreateMemberResponseDto>.Succeed(member));
        }

        /// <summary>
        /// Updates a member for the provided tenant
        /// </summary>
        /// <param name="memberId">Id of the member to update for the tenant</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<UpdateMemberResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{memberId:int}")]
        public async Task<IActionResult> UpdateMember(int memberId,
                                                      [FromBody] UpdateMemberRequestDto request)
        {
            var tenantId = 0;
            if (request is null || tenantId <= 0 || request.MemberId <= 0)
                return BadRequest("Invalid request");

            if (request!.TenantId != tenantId || request.MemberId != memberId)
                return BadRequest("Invalid request");

            var department = await _updateMemberCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<UpdateMemberResponseDto>.Succeed(department));
        }

        /// <summary>
        /// Deletes a member for the provided tenant
        /// </summary>
        /// <param name="memberId">Id the of the member to delete for the tenant</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{memberId:int}")]
        public async Task<IActionResult> DeleteMember(int memberId)
        {
            var tenantId = 0;
            if (tenantId <= 0 || memberId <= 0)
                BadRequest("Invalid request");

            await _deleteMemberCommand.ExecuteAsync(memberId, tenantId);

            return Ok(ApiRequestResponse<string>.Succeed("Deleted successfully"));
        }
    }
}