using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Queries.Tenant;
using Application.Queries.Tenant.TenantDashboardData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IQueryTenantDashboardData _queryTenantDashboardData;

        public DashboardController(IQueryTenantDashboardData queryTenantDashboardData)
        {
            _queryTenantDashboardData = queryTenantDashboardData;
        }


        [ProducesResponseType(typeof(ApiRequestResponse<DashboardDataDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public async Task<IActionResult> GetMonthDashboard()
        {
            int tenantId = 0;
            var response = await _queryTenantDashboardData.ExecuteAsync(tenantId);

            if (response is null)
                return NotFound(ApiRequestResponse<DashboardDataDto>.Fail("Not found"));

            var result = ApiRequestResponse<DashboardDataDto>.Succeed(response.Result);
            return Ok(result);
        }
    }
}