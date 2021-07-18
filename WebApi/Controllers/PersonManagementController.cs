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
    public class PersonManagementController : ControllerBase
    {
        private readonly IQueryPersonManagement _personManagementQuery;
        private readonly ICreateDepartmentCommand _createDepartmentCommand;
        private readonly IUpdateDepartmentCommand _updateDepartmentCommand;
        private readonly IDeleteDepartmentCommand _deleteDepartmentCommand;
        private readonly ICreateMemberCommand _createMemberCommand;
        private readonly IUpdateMemberCommand _updateMemberCommand;
        private readonly IDeleteMemberCommand _deleteMemberCommand;

        public PersonManagementController(IQueryPersonManagement personManagementQuery,
                                          ICreateDepartmentCommand createDepartmentCommand,
                                          IUpdateDepartmentCommand updateDepartmentCommand,
                                          IDeleteDepartmentCommand deleteDepartmentCommand,
                                          ICreateMemberCommand createMemberCommand,
                                          IUpdateMemberCommand updateMemberCommand,
                                          IDeleteMemberCommand deleteMemberCommand)
        {
            _personManagementQuery = personManagementQuery;
            _createDepartmentCommand = createDepartmentCommand;
            _updateDepartmentCommand = updateDepartmentCommand;
            _deleteDepartmentCommand = deleteDepartmentCommand;
            _createMemberCommand = createMemberCommand;
            _updateMemberCommand = updateMemberCommand;
            _deleteMemberCommand = deleteMemberCommand;
        }

        #region Department

        /// <summary>
        /// Gets all departments for provided tenantId
        /// </summary>
        /// <param name="tenantId">The id of the tenant which you want to get its departments</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<GetDepartmentsResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("tenant/{tenantId:int}/departments")]
        public async Task<IActionResult> GetDepartments(int tenantId)
        {
            if (tenantId <= 0)
                return BadRequest("Invalid tenantId");

            var queryResult =
                await _personManagementQuery.GetDepartmentsByTenantIdAsync(tenantId);

            if (!queryResult.Results.Any())
                return NotFound(ApiRequestResponse<GetDepartmentsResponseDto>
                                    .Fail($"No departments found for tenant {tenantId}"));

            return Ok(ApiRequestResponse<GetDepartmentsResponseDto>.Succeed(queryResult.Results
                          .ToList()));
        }

        /// <summary>
        /// Creates a new department for the provided tenant
        /// </summary>
        /// <param name="tenantId">Id of tenant you're creating the department for. </param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<CreateDepartmentResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("tenant/{tenantId}/departments")]
        public async Task<IActionResult> CreateDepartment(int tenantId,
                                                          [FromBody]
                                                          CreateDepartmentRequestDto request)
        {
            if (request is null || tenantId <= 0)
                return BadRequest("Invalid request");

            if (request!.TenantId != tenantId)
                return BadRequest("Invalid request");

            var department =
                await _createDepartmentCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<CreateDepartmentResponseDto>.Succeed(department));
        }

        /// <summary>
        /// Updates a department for the provided tenant
        /// </summary>
        /// <param name="tenantId">Id of tenant you're updating the department in. </param>
        /// <param name="departmentId">Id of the department to update for the tenant</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<UpdateDepartmentResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("tenant/{tenantId:int}/departments/{departmentId:int}")]
        public async Task<IActionResult> UpdateDepartment(int tenantId,
                                                          int departmentId,
                                                          [FromBody]
                                                          UpdateDepartmentRequestDto request)
        {
            if (request is null || tenantId <= 0 || request.DepartmentId <= 0)
                return BadRequest("Invalid request");

            if (request!.TenantId != tenantId || request.DepartmentId != departmentId)
                return BadRequest("Invalid request");

            var department = await _updateDepartmentCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<UpdateDepartmentResponseDto>.Succeed(department));
        }

        /// <summary>
        /// Deletes a department for the provided tenant
        /// </summary>
        /// <param name="tenantId">Id of tenant to delete the department for. </param>
        /// <param name="departmentId">Id the of the department to delete for the tenant</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("tenant/{tenantId:int}/departments/{departmentId:int}")]
        public async Task<IActionResult> DeleteDepartment(int tenantId,
                                                          int departmentId
            )
        {
            if (tenantId <= 0 || departmentId <= 0)
                BadRequest("Invalid request");

            await _deleteDepartmentCommand.ExecuteAsync(departmentId, tenantId);

            return Ok(ApiRequestResponse<string>.Succeed("Deleted successfully"));
        }

        #endregion

        #region Members

        /// <summary>
        /// Gets all members for provided tenantId
        /// </summary>
        /// <param name="tenantId">The id of the tenant which you want to get its members</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<GetMembersResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("tenant/{tenantId:int}/members")]
        public async Task<IActionResult> GetMembers(int tenantId)
        {
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
        /// <param name="tenantId">Id of tenant you're creating the member for. </param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<CreateMemberResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("tenant/{tenantId}/members")]
        public async Task<IActionResult> CreateMember(int tenantId,
                                                      [FromBody] CreateMemberRequestDto request)
        {
            if (request is null || tenantId <= 0)
                return BadRequest("Invalid request");

            if (request!.TenantId != tenantId)
                return BadRequest("Invalid request");

            var department =
                await _createMemberCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<CreateMemberResponseDto>.Succeed(department));
        }

        /// <summary>
        /// Updates a member for the provided tenant
        /// </summary>
        /// <param name="tenantId">Id of tenant you're updating the member for. </param>
        /// <param name="memberId">Id of the member to update for the tenant</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<UpdateMemberResponseDto>),
                                 StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("tenant/{tenantId:int}/members/{memberId:int}")]
        public async Task<IActionResult> UpdateMember(int tenantId,
                                                      int memberId,
                                                      [FromBody] UpdateMemberRequestDto request)
        {
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
        /// <param name="tenantId">Id of tenant to delete the member for. </param>
        /// <param name="memberId">Id the of the member to delete for the tenant</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("tenant/{tenantId:int}/members/{memberId:int}")]
        public async Task<IActionResult> DeleteMember(int tenantId,
                                                      int memberId)
        {
            if (tenantId <= 0 || memberId <= 0)
                BadRequest("Invalid request");

            await _deleteMemberCommand.ExecuteAsync(memberId, tenantId);

            return Ok(ApiRequestResponse<string>.Succeed("Deleted successfully"));
        }

        #endregion

        #region Ministers

        

        #endregion
    }
}