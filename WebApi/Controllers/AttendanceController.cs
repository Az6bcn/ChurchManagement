using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Attendance.Create;
using Application.Commands.Attendance.Delete;
using Application.Commands.Attendance.Update;
using Application.Commands.Finance.Create;
using Application.Commands.Finance.Delete;
using Application.Commands.Finance.Update;
using Application.Commands.PersonManagement.Create;
using Application.Dtos.Request.Create;
using Application.Dtos.Request.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly ICreateAttendanceCommand _createAttendanceCommand;
        private readonly IUpdateAttendanceCommand _updateAttendanceCommand;
        private readonly IDeleteAttendanceCommand _deleteAttendanceCommand;

        public AttendanceController(ICreateAttendanceCommand createAttendanceCommand,
                                    IUpdateAttendanceCommand updateAttendanceCommand,
                                    IDeleteAttendanceCommand deleteAttendanceCommand)
        {
            _createAttendanceCommand = createAttendanceCommand;
            _updateAttendanceCommand = updateAttendanceCommand;
            _deleteAttendanceCommand = deleteAttendanceCommand;
        }

        /// <summary>
        /// Create a new attendance entry for the tenant.
        /// </summary>
        /// <param name="tenantId">Tenant to create the attendance entry record for</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [HttpPost("{tenant:int}")]
        public async Task<IActionResult> CreateAttendance(int tenantId, [FromBody] CreateAttendanceRequestDto request)
        {
            if (request is null)
                return BadRequest("Invalid request");

            await _createAttendanceCommand.ExecuteAsync(request);
            
            return Ok(ApiRequestResponse<string>.Succeed("Created successfully"));
        }


        /// <summary>
        /// Updates attendance for the indicated tenant
        /// </summary>
        /// <param name="tenantId">Tenant to update the attendance record for</param>
        /// <param name="attendanceId">Attendance record Id to be updated</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [HttpPut("{tenant:int}/attendances/{attendanceId:int}")]
        public async Task<IActionResult> UpdateAttendance(int tenantId, int attendanceId, [FromBody] 
        UpdateAttendanceRequestDto 
        request)
        {
            if (tenantId != request.TenantId || attendanceId != request.AttendanceId)
                return BadRequest("Invalid request");

            await _updateAttendanceCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<string>.Succeed("Updated Successfuly"));
        }


        /// <summary>
        /// Deletes attendance for the indicated tenant
        /// </summary>
        /// <param name="tenantId">Tenant to delete the finance record for</param>
        /// <param name="attendanceId">Attendance record Id to be deleted</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [HttpDelete("{tenant:int}/attendances/{attendanceId:int}")]
        public async Task<IActionResult> DeleteFinance(int tenantId, int attendanceId)
        {
            if (tenantId != request.TenantId || attendanceId != request.FinanceId)
                return BadRequest("Invalid request");

            await _deleteAttendanceCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<string>.Succeed("Updated Successfuly"));
        }
    }
}