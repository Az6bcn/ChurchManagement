using Application.Dtos.Request.Update;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Queries.Finances;
using Application.Queries.Tenants;
using AutoMapper;
using Domain.Validators;

namespace Application.Commands.Finances.Update;

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
                       request.Description,
                       out var notification);

        if (notification.HasErrors)
            throw new ValidationException("Request failed validation", notification.Errors);

        _financeRepo.Update(finance);
        await _unitOfWork.SaveChangesAsync();
    }
}