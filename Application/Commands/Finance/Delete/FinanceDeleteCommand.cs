using Application.Dtos.Request.Update;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.Finance;
using Application.Queries.Tenant;
using AutoMapper;
using Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Finance.Delete
{
    public class FinanceDeleteCommand: IDeleteFinanceCommand
    {
        private readonly IQueryFinance _financeQuery;
        private readonly IFinanceRepositoryAsync _financeRepo;
        private readonly IQueryTenant _tenantQuery;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidateFinanceInDomain _validator;
        private readonly IMapper _mapper;

        public FinanceDeleteCommand(IQueryFinance financeQuery,
                                     IFinanceRepositoryAsync financeRepo,
                                     IQueryTenant tenantQuery,
                                     IUnitOfWork unitOfWork,
                                     IValidateFinanceInDomain validator,
                                     IMapper mapper)
        {
            _financeQuery = financeQuery;
            _financeRepo = financeRepo;
            _tenantQuery = tenantQuery;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(int financeId, int tenantId)
        {
            var finance = await _financeQuery.GetFinanceByIdAndTenantIdAsync(financeId, tenantId);
            if (finance is null)
                throw new InvalidOperationException($"Finace {financeId} not found");

            finance.Delete();

            _financeRepo.Update(finance);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
