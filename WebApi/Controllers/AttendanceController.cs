using Application.Commands.Attendances.Create;
using Application.Commands.Attendances.Delete;
using Application.Commands.Attendances.Update;
using Application.Dtos.Request.Create;
using Application.Dtos.Request.Update;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendancesController : ControllerBase
{
    private readonly ICreateAttendanceCommand _createAttendanceCommand;
    private readonly IUpdateAttendanceCommand _updateAttendanceCommand;
    private readonly IDeleteAttendanceCommand _deleteAttendanceCommand;

    public AttendancesController(ICreateAttendanceCommand createAttendanceCommand,
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
    /// <param name="request">The request object</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAttendance([FromBody] CreateAttendanceRequestDto request)
    {
        var tenantId = HttpContext.GetTenantId();

        if (request is null || tenantId != request.TenantId)
            return BadRequest("Invalid request");

        await _createAttendanceCommand.ExecuteAsync(request);
            
        return Ok(ApiRequestResponse<string>.Succeed("Created successfully"));
    }


    /// <summary>
    /// Updates attendance for the indicated tenant
    /// </summary>
    /// <param name="attendanceId">Attendance record Id to be updated</param>
    /// <param name="request">The request object</param>
    /// <returns></returns>
    [HttpPut("{attendanceId:int}")]
    public async Task<IActionResult> UpdateAttendance(int attendanceId,
                                                      [FromBody] UpdateAttendanceRequestDto request)
    {
        var tenantId = HttpContext.GetTenantId();
            
        if (tenantId != request.TenantId || attendanceId != request.AttendanceId)
            return BadRequest("Invalid request");

        await _updateAttendanceCommand.ExecuteAsync(request);

        return Ok(ApiRequestResponse<string>.Succeed("Updated Successfully"));
    }


    /// <summary>
    /// Deletes attendance for the indicated tenant
    /// </summary>
    /// <param name="attendanceId">Attendance record Id to be deleted</param>
    /// <returns></returns>
    [HttpDelete("{attendanceId:int}")]
    public async Task<IActionResult> DeleteFinance(int attendanceId)
    {
        var tenantId = HttpContext.GetTenantId();

        if (tenantId <= 0 || attendanceId <= 0)
            return BadRequest("Invalid request");

        await _deleteAttendanceCommand.ExecuteAsync(attendanceId, tenantId);

        return Ok(ApiRequestResponse<string>.Succeed("Updated Successfully"));
    }
}