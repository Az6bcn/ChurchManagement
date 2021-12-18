using Application.Dtos.Response.Get;
using Application.Queries.Tenants.TenantDashboardData;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IQueryTenantDashboardData _queryTenantDashboardData;

    public DashboardController(IQueryTenantDashboardData queryTenantDashboardData)
    {
        _queryTenantDashboardData = queryTenantDashboardData;
    }
        
    /// <summary>
    /// Get dashboard data for the month
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiRequestResponse<GetDashboardDataResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet()]
    public async Task<IActionResult> GetMonthDashboard()
    {
        var tenantId = HttpContext.GetTenantId();
        var tenantCurrencyCode = HttpContext.GetTenantCurrencyCode();
            
        var response
            = await _queryTenantDashboardData.ExecuteAsync(tenantId,
                                                           tenantCurrencyCode,
                                                           startDate: null,
                                                           endDate: null);

        if (response.Result is null)
            return NotFound(ApiRequestResponse<GetDashboardDataResponseDto>.Fail("Not found"));

        var result 
            = ApiRequestResponse<GetDashboardDataResponseDto>.Succeed(response.Result);
            
        return Ok(result);
    }

    /// <summary>
    /// Get dashboard data for the month, if start and end date not provides
    /// </summary>
    /// <param name="startDate">start date for the dashboard data</param>
    /// <param name="endDate">end date for the dashboard data</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiRequestResponse<GetDashboardDataResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("from/{startDate}/to/{endDate}")]
    public async Task<IActionResult> GetMonthDashboard(DateTime? startDate = null, DateTime? endDate = null)
    {
        var tenantId = HttpContext.GetTenantId();
        var tenantCurrencyCode = HttpContext.GetTenantCurrencyCode();
            
        var response
            = await _queryTenantDashboardData.ExecuteAsync(tenantId,
                                                           tenantCurrencyCode,
                                                           startDate,
                                                           endDate);

        if (response.Result is null)
            return NotFound(ApiRequestResponse<GetDashboardDataResponseDto>.Fail("Not found"));

        var result 
            = ApiRequestResponse<GetDashboardDataResponseDto>.Succeed(response.Result);
            
        return Ok(result);
    }
}