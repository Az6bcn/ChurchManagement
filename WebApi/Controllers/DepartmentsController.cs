using Application.Commands.PersonManagements.Create;
using Application.Commands.PersonManagements.Delete;
using Application.Commands.PersonManagements.Update;
using Application.Dtos.Request.Create;
using Application.Dtos.Request.Update;
using Application.Dtos.Response.Create;
using Application.Dtos.Response.Get;
using Application.Dtos.Response.Update;
using Application.Queries.PersonManagements;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IQueryPersonManagement _personManagementQuery;
    private readonly ICreateDepartmentCommand _createDepartmentCommand;
    private readonly IUpdateDepartmentCommand _updateDepartmentCommand;
    private readonly IDeleteDepartmentCommand _deleteDepartmentCommand;
    private readonly IAssignMemberToDepartmentCommand _assignMemberToDepartmentCommand;
    private readonly IUnAssignMemberFromDepartment _assignMemberFromDepartment;
    private readonly IUnAssignHeadOfDepartmentCommand _unAssignHeadOfDepartmentCommand;
    private readonly IAssignHeadOfDepartmentCommand _assignHeadOfDepartmentCommand;

    public DepartmentsController(IQueryPersonManagement personManagementQuery,
                                 ICreateDepartmentCommand createDepartmentCommand,
                                 IUpdateDepartmentCommand updateDepartmentCommand,
                                 IDeleteDepartmentCommand deleteDepartmentCommand,
                                 IAssignMemberToDepartmentCommand assignMemberToDepartmentCommand,
                                 IUnAssignMemberFromDepartment assignMemberFromDepartment,
                                 IUnAssignHeadOfDepartmentCommand unAssignHeadOfDepartmentCommand,
                                 IAssignHeadOfDepartmentCommand assignHeadOfDepartmentCommand)
    {
        _personManagementQuery = personManagementQuery;
        _createDepartmentCommand = createDepartmentCommand;
        _updateDepartmentCommand = updateDepartmentCommand;
        _deleteDepartmentCommand = deleteDepartmentCommand;
        _assignMemberToDepartmentCommand = assignMemberToDepartmentCommand;
        _assignMemberFromDepartment = assignMemberFromDepartment;
        _unAssignHeadOfDepartmentCommand = unAssignHeadOfDepartmentCommand;
        _assignHeadOfDepartmentCommand = assignHeadOfDepartmentCommand;
    }

    /// <summary>
    /// Gets all departments for provided tenantId
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiRequestResponse<GetDepartmentsResponseDto>),
                             StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet()]
    public async Task<IActionResult> GetDepartments()
    {
        var tenantId = HttpContext.GetTenantId();

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
    /// <param name="request">The request object</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiRequestResponse<CreateDepartmentResponseDto>),
                             StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost()]
    public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequestDto request)
    {
        var tenantId = HttpContext.GetTenantId();

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
    /// <param name="departmentId">Id of the department to update for the tenant</param>
    /// <param name="request">The request object</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiRequestResponse<UpdateDepartmentResponseDto>),
                             StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("{departmentId:int}")]
    public async Task<IActionResult> UpdateDepartment(int departmentId,
                                                      [FromBody] UpdateDepartmentRequestDto request)
    {
        var tenantId = HttpContext.GetTenantId();

        if (request is null || tenantId <= 0 || request.DepartmentId <= 0 || tenantId != request.TenantId)
            return BadRequest("Invalid request");

        if (request!.TenantId != tenantId || request.DepartmentId != departmentId)
            return BadRequest("Invalid request");

        var department = await _updateDepartmentCommand.ExecuteAsync(request);

        return Ok(ApiRequestResponse<UpdateDepartmentResponseDto>.Succeed(department));
    }

    /// <summary>
    /// Deletes a department for the provided tenant
    /// </summary>
    /// <param name="departmentId">Id the of the department to delete for the tenant</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{departmentId:int}")]
    public async Task<IActionResult> DeleteDepartment(int departmentId)
    {
        var tenantId = HttpContext.GetTenantId();

        if (tenantId <= 0 || departmentId <= 0)
            BadRequest("Invalid request");

        await _deleteDepartmentCommand.ExecuteAsync(departmentId, tenantId);

        return Ok(ApiRequestResponse<string>.Succeed("Deleted successfully"));
    }


    /// <summary>
    /// Assigns a member to a department.
    /// </summary>
    /// <param name="departmentId"> Department to assign member to.</param>
    /// <param name="memberId"></param>
    /// <param name="request">The request object</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{departmentId:int}/Members/{memberId:int}")]
    public async Task<IActionResult> AssignMemberToDepartment(int departmentId,
                                                              int memberId,
                                                              [FromBody] AssignMemberToDepartmentRequestDto request)
    {
        var tenantId = HttpContext.GetTenantId();

        if (departmentId <= 0 || departmentId != request.DepartmentId 
                              || tenantId != request.TenantId || memberId != request.MemberId)
            return BadRequest("Invalid request");

        await _assignMemberToDepartmentCommand.ExecuteAsync(request);

        return Ok(ApiRequestResponse<string>.Succeed($"Assigned memberId to department successfully"));
    }

    /// <summary>
    /// Un-assigns a member from a department.
    /// </summary>
    /// <param name="departmentId"> Department to un-assign member from.</param>
    /// <param name="memberId"></param>
    /// <param name="request">The request object</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("{departmentId:int}/Members/{memberId:int}")]
    public async Task<IActionResult> UnAssignMemberToDepartment(int departmentId,
                                                                int memberId,
                                                                [FromBody] AssignMemberToDepartmentRequestDto request)
    {
        var tenantId = HttpContext.GetTenantId();

        if (departmentId <= 0 || departmentId != request.DepartmentId 
                              || tenantId != request.TenantId || memberId != request.MemberId)
            return BadRequest("Invalid request");

        await _unAssignHeadOfDepartmentCommand.ExecuteAsync(request);

        return Ok(ApiRequestResponse<string>.Succeed($"Member unassigned from department successfully"));
    }

    /// <summary>
    /// Assigns a member as the HOD a department.
    /// </summary>
    /// <param name="departmentId"> Department to assign the member to as HOD.</param>
    /// <param name="memberId"></param>
    /// <param name="request">The request object</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{departmentId:int}/members/{memberId:int}/hod")]
    public async Task<IActionResult> AssignAsHod(int departmentId,
                                                 int memberId,
                                                 [FromBody] AssignMemberToDepartmentRequestDto request)
    {
        var tenantId = HttpContext.GetTenantId();
            
        if (departmentId <= 0 || departmentId != request.DepartmentId 
                              || tenantId != request.TenantId || memberId != request.MemberId)
            return BadRequest("Invalid request");

        await _assignHeadOfDepartmentCommand.ExecuteAsync(request);

        return Ok(ApiRequestResponse<string>.Succeed($"Member assigned as department HOD successfully"));
    }

    /// <summary>
    /// Removes a member as the HOD a department.
    /// </summary>
    /// <param name="departmentId"> Department to remove the member from as HOD.</param>
    /// <param name="memberId"></param>
    /// <param name="request">The request object</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiRequestResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("{departmentId:int}/members/{memberId:int}/hod")]
    public async Task<IActionResult> RemoveAsHod(int departmentId,
                                                 int memberId,
                                                 [FromBody] AssignMemberToDepartmentRequestDto request)
    {
        var tenantId = HttpContext.GetTenantId();

        if (departmentId <= 0 || departmentId != request.DepartmentId 
                              || tenantId != request.TenantId || memberId != request.MemberId)
            return BadRequest("Invalid request");

        await _unAssignHeadOfDepartmentCommand.ExecuteAsync(request);

        return Ok(ApiRequestResponse<string>.Succeed($"Member removed as department HOD successfully"));
    }
}