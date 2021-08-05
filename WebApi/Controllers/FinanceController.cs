using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Finance.Create;
using Application.Commands.PersonManagement.Create;
using Application.Dtos.Request.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly ICreateFinanceCommand _createFinanceCommand;

        public FinanceController(ICreateFinanceCommand createFinanceCommand)
        {
            _createFinanceCommand = createFinanceCommand;
        }

        /// <summary>
        /// Create a new finance entry for the tenant.
        /// </summary>
        /// <param name="tenantId">Tenant to create the finance entry record for</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [HttpPost("{tenant:int}")]
        public async Task<IActionResult> CreateFinance(int tenantId, [FromBody] CreateFinanceRequestDto request)
        {
            if (request is null)
                return BadRequest("Invalid request");

            await _createFinanceCommand.ExecuteAsync(request);
            
            return Ok(ApiRequestResponse<string>.Succeed("Created successfully"));
        }
    }
}