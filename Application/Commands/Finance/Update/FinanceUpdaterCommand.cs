using System;
using System.Threading.Tasks;
using Application.Dtos.Request.Update;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.Finance;
using Application.Queries.Tenant;
using AutoMapper;
using Domain.Validators;

namespace Application.Commands.Finance.Update
{
    public class FinanceUpdaterCommand: IUpdateFinanceCommand
    {
        private readonly IQueryFinance _financeQuery;
        private readonly IFinanceRepositoryAsync _financeRepo;
        private readonly IQueryTenant _tenantQuery;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidateFinanceInDomain _validator;
        private readonly IMapper _mapper;

        public FinanceUpdaterCommand(IQueryFinance financeQuery,
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

        public async Task ExecuteAsync(UpdateFinanceRequestDto request)
        {
            var finance = await _financeQuery.GetFinanceByIdAndTenantIdAsync(request.FinanceId, request.TenantId);
            if (finance is null)
                throw new InvalidOperationException($"Finance {request.FinanceId} not found");
            
            finance.Update(_validator,
                        finance.Tenant,
                        request.Amount,
                        request.FinanceTypeEnum,
                        request.ServiceTypeEnum,
                        request.CurrencyTypeEnum,
                        request.GivenDate,
                        request.Description);

            _financeRepo.Update(finance);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}