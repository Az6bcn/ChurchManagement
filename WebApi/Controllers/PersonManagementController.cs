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

        public PersonManagementController(IQueryPersonManagement personManagementQuery,
                                          ICreateDepartmentCommand createDepartmentCommand,
                                          IUpdateDepartmentCommand updateDepartmentCommand,
                                          IDeleteDepartmentCommand deleteDepartmentCommand)
        {
            _personManagementQuery = personManagementQuery;
            _createDepartmentCommand = createDepartmentCommand;
            _updateDepartmentCommand = updateDepartmentCommand;
            _deleteDepartmentCommand = deleteDepartmentCommand;
        }

        /// <summary>
        /// Gets all departments for provided tenantId
        /// </summary>
        /// <param name="tenantId">The id of the tenant which you want to get its departments</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<GetDepartmentsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{tenantId:int}/departments")]
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
        [HttpPost("{tenantId}/departments")]
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

        /// <summary>
        /// Updates a department for the provided tenant
        /// </summary>
        /// <param name="tenantId">Id of tenant you're updating the department in. </param>
        /// <param name="departmentId">Id of the department to update for the tenant</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiRequestResponse<UpdateDepartmentResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{tenantId:int}/departments/{departmentId:int}")]
        public async Task<IActionResult> UpdateDepartment(int tenantId,
                                                          int departmentId,
                                                          [FromBody] UpdateDepartmentRequestDto request)
        {
            if (request is null || tenantId <= 0 || request.DepartmentId <= 0)
                BadRequest("Invalid request");

            if (request!.TenantId != tenantId)
                BadRequest("Invalid request");

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
        [HttpDelete("{tenantId:int}/departments/{departmentId:int}")]
        public async Task<IActionResult> DeleteDepartment(int tenantId,
                                                          int departmentId
                                                          )
        {
            if (tenantId <= 0 || departmentId <= 0)
                BadRequest("Invalid request");

            await _deleteDepartmentCommand.ExecuteAsync(departmentId, tenantId);

            return Ok(ApiRequestResponse<string>.Succeed("Deleted successfully"));
        }
    }
}