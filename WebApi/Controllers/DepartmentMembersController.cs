using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PersonManagement.Create;
using Application.Dtos.Request.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentMembersController : ControllerBase
    {
        private readonly IAssignMemberToDepartmentCommand _assignMemberToDepartmentCommand;
        private readonly IUnAssignMemberFromDepartment _assignMemberFromDepartment;
        private readonly IUnAssignHeadOfDepartmentCommand _unAssignHeadOfDepartmentCommand;
        private readonly IAssignHeadOfDepartmentCommand _assignHeadOfDepartmentCommand;

        public DepartmentMembersController(IAssignMemberToDepartmentCommand assignMemberToDepartmentCommand,
                                           IUnAssignMemberFromDepartment assignMemberFromDepartment,
                                           IUnAssignHeadOfDepartmentCommand unAssignHeadOfDepartmentCommand,
                                           IAssignHeadOfDepartmentCommand assignHeadOfDepartmentCommand)
        {
            _assignMemberToDepartmentCommand = assignMemberToDepartmentCommand;
            _assignMemberFromDepartment = assignMemberFromDepartment;
            _unAssignHeadOfDepartmentCommand = unAssignHeadOfDepartmentCommand;
            _assignHeadOfDepartmentCommand = assignHeadOfDepartmentCommand;
        }

        /// <summary>
        /// Assigns a member to a department.
        /// </summary>
        /// <param name="departmentId"> Department to assign member to.</param>
        /// <param name="memberId">Member to assign to the department</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("department/{departmentId:int}/assign/member/{memberId:int}")]
        public async Task<IActionResult> AssignMemberToDepartment(int departmentId,
                                                                  int memberId,
                                                                  [FromBody] AssignMemberToDepartmentRequestDto request)
        {
            if (departmentId <= 0 || memberId <= 0
                                  || departmentId != request.DepartmentId
                                  || memberId != request.MemberId)
                return BadRequest("Invalid department od member Id");

            await _assignMemberToDepartmentCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<string>.Succeed($"Assigned memberId to department successfully"));
        }

        /// <summary>
        /// Unassigns a member from a department.
        /// </summary>
        /// <param name="departmentId"> Department to un-assign member from.</param>
        /// <param name="memberId">Member to un-assign to the department</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("department/{departmentId:int}/unassign/member/{memberId:int}")]
        public async Task<IActionResult> UnAssignMemberToDepartment(int departmentId,
                                                                    int memberId,
                                                                    [FromBody]
                                                                    AssignMemberToDepartmentRequestDto request)
        {
            if (departmentId <= 0 || memberId <= 0
                                  || departmentId != request.DepartmentId
                                  || memberId != request.MemberId)
                return BadRequest("Invalid department or member Id");

            await _unAssignHeadOfDepartmentCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<string>.Succeed($"Member unassigned to department successfully"));
        }
        
        /// <summary>
        /// Assigns a member as the HOD a department.
        /// </summary>
        /// <param name="departmentId"> Department to assign the member to as HOD.</param>
        /// <param name="memberId">Member to assign as the HOD of the department</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("department/{departmentId:int}/assignhod/member/{memberId:int}")]
        public async Task<IActionResult> AssignAsHod(int departmentId,
                                                     int memberId,
                                                     [FromBody] AssignMemberToDepartmentRequestDto request)
        {
            if (departmentId <= 0 || memberId <= 0
                                  || departmentId != request.DepartmentId
                                  || memberId != request.MemberId)
                return BadRequest("Invalid department or member Id");

            await _assignHeadOfDepartmentCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<string>.Succeed($"Member assigned as department HOD successfully"));
        }

        /// <summary>
        /// Removes a member as the HOD a department.
        /// </summary>
        /// <param name="departmentId"> Department to remove the member from as HOD.</param>
        /// <param name="memberId">Member to remove as HOD of the department</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("department/{departmentId:int}/unassignhod/member/{memberId:int}")]
        public async Task<IActionResult> RemoveAsHod(int departmentId,
                                                     int memberId,
                                                     [FromBody] AssignMemberToDepartmentRequestDto request)
        {
            if (departmentId <= 0 || memberId <= 0
                                  || departmentId != request.DepartmentId
                                  || memberId != request.MemberId)
                return BadRequest("Invalid department or member Id");

            await _unAssignHeadOfDepartmentCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<string>.Succeed($"Member removed as department HOD successfully"));
        }
    }
}