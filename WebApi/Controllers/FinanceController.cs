using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class FinanceController : ControllerBase
    {
        private readonly ICreateFinanceCommand _createFinanceCommand;
        private readonly IUpdateFinanceCommand _updateFinanceCommand;
        private readonly IDeleteFinanceCommand _deleteFinanceCommand;

        public FinanceController(
            ICreateFinanceCommand createFinanceCommand, IUpdateFinanceCommand updateFinanceCommand,
            IDeleteFinanceCommand deleteFinanceCommand)
        {
            _createFinanceCommand = createFinanceCommand;
            _updateFinanceCommand = updateFinanceCommand;
            _deleteFinanceCommand = deleteFinanceCommand;
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


        /// <summary>
        /// Updates finance for the indicated tenant
        /// </summary>
        /// <param name="tenantId">Tenant to update the finance record for</param>
        /// <param name="financeId">Finance record Id to be updated</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [HttpPut("{tenant:int}/finances/{financeId:int}")]
        public async Task<IActionResult> UpdateFinance(int tenantId, int financeId, [FromBody] UpdateFinanceRequestDto request)
        {
            if (tenantId != request.TenantId || financeId != request.FinanceId)
                return BadRequest("Invalid request");

            await _updateFinanceCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<string>.Succeed("Updated Successfuly"));
        }


        /// <summary>
        /// Deletes finance for the indicated tenant
        /// </summary>
        /// <param name="tenantId">Tenant to delete the finance record for</param>
        /// <param name="financeId">Finance record Id to be deleted</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [HttpDelete("{tenant:int}/finances/{financeId:int}")]
        public async Task<IActionResult> DeleteFinance(int tenantId, int financeId, [FromBody] UpdateFinanceRequestDto request)
        {
            if (tenantId != request.TenantId || financeId != request.FinanceId)
                return BadRequest("Invalid request");

            await _updateFinanceCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<string>.Succeed("Updated Successfuly"));
        }
    }
}