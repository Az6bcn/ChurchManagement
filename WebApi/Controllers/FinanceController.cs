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
    public class FinancesController : ControllerBase
    {
        private readonly ICreateFinanceCommand _createFinanceCommand;
        private readonly IUpdateFinanceCommand _updateFinanceCommand;
        private readonly IDeleteFinanceCommand _deleteFinanceCommand;

        public FinancesController(
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
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateFinance([FromBody] CreateFinanceRequestDto request)
        {
            var tenantId = HttpContext.GetTenantId();

            if (request is null)
                return BadRequest("Invalid request");

            await _createFinanceCommand.ExecuteAsync(request);
            
            return Ok(ApiRequestResponse<string>.Succeed("Created successfully"));
        }


        /// <summary>
        /// Updates finance for the indicated tenant
        /// </summary>
        /// <param name="financeId">Finance record Id to be updated</param>
        /// <param name="request">The request object</param>
        /// <returns></returns>
        [HttpPut("{financeId:int}")]
        public async Task<IActionResult> UpdateFinance(int financeId, [FromBody] UpdateFinanceRequestDto request)
        {
            var tenantId = HttpContext.GetTenantId();

            if (tenantId != request.TenantId || financeId != request.FinanceId)
                return BadRequest("Invalid request");

            await _updateFinanceCommand.ExecuteAsync(request);

            return Ok(ApiRequestResponse<string>.Succeed("Updated Successfully"));
        }


        /// <summary>
        /// Deletes finance for the indicated tenant
        /// </summary>
        /// <param name="financeId">Finance record Id to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{financeId:int}")]
        public async Task<IActionResult> DeleteFinance(int financeId)
        {
            var tenantId = HttpContext.GetTenantId();

            if (tenantId <= 0 || financeId <= 0)
                return BadRequest("Invalid request");

            await _deleteFinanceCommand.ExecuteAsync(financeId, tenantId);

            return Ok(ApiRequestResponse<string>.Succeed("Updated Successfully"));
        }
    }
}