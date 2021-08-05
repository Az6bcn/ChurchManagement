using System;
using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.Finance;
using Application.Queries.Tenant;
using AutoMapper;
using Domain.Validators;

namespace Application.Commands.Finance.Create
{
    public class FinanceCreatorCommand : ICreateFinanceCommand
    {
        private readonly IQueryFinance _financeQuery;
        private readonly IFinanceRepositoryAsync _financeRepo;
        private readonly IQueryTenant _tenantQuery;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidateFinanceInDomain _validator;
        private readonly IMapper _mapper;

        public FinanceCreatorCommand(IQueryFinance financeQuery,
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

        public async Task ExecuteAsync(CreateFinanceRequestDto request)
        {
            var tenant = await _tenantQuery.GetTenantByIdAsync(request.TenantId);
            if (tenant is null)
                throw new ArgumentException("Invalid tenantId", nameof(request.TenantId));

            var finance = Domain.Entities.FinanceAggregate.Finance.Create(_validator,
                                                                          tenant,
                                                                          request.Amount,
                                                                          request.FinanceTypeEnum,
                                                                          request.ServiceTypeEnum,
                                                                          request.CurrencyTypeEnum,
                                                                          request.GivenDate,
                                                                          request.Description);

            await _financeRepo.AddAsync(finance);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}