using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PersonManagement.Create;
using Application.Dtos.Response.Get;
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
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpGet("departments/{tenantId:int}")]
        public async Task<IActionResult> GetDepartments(int tenantId)
        {
            if (tenantId <= 0)
                return BadRequest("Invalid tenantId");

            var departments = 
                await _personManagementQuery.GetDepartmentsByTenantIdAsync(tenantId);

            if (!departments.Any())
                return NotFound(ApiRequestResponse<GetDepartmentsResponseDto>
                                    .Fail($"No departments found for tenant {tenantId}");
            
            
        }
    }
}