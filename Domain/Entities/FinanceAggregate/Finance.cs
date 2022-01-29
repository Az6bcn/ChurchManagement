using Domain.Entities.Helpers;
using Domain.Entities.TenantAggregate;
using Domain.Interfaces;
using Domain.Validators;
using Domain.ValueObjects;
using Shared.Communications;
using Shared.Enums;

namespace Domain.Entities.FinanceAggregate;

public class Finance : IEntity, IAggregateRoot
{
    public Finance()
    {
            
    }
        
    internal Finance(Tenant tenant,
                     decimal amount, 
                     FinanceEnum financeEnum,
                     ServiceEnum serviceEnum,
                     CurrencyEnum currencyEnum,
                     DateOnly givenDate,
                     string? description)
    {    
        var serviceEnumValue = GetServiceTypeEnumValue(serviceEnum);
        var financeEnumValue = GetFinanceTypeEnumValue(financeEnum);
        var currencyEnumValue = GetCurrencyTypeEnumValue(currencyEnum);

        ServiceType = ServiceType.Create(serviceEnumValue.Id, serviceEnumValue.Value);
        FinanceType = FinanceType.Create(financeEnumValue.Id, financeEnumValue.Value);
        Currency = Currency.Create(currencyEnumValue.Id, currencyEnumValue.Value);

        Tenant = tenant;
        TenantId = tenant.TenantId;
        FinanceTypeId = financeEnumValue.Id;
        ServiceTypeId = serviceEnumValue.Id;
        CurrencyId = currencyEnumValue.Id;
        Amount = amount;
        GivenDate = givenDate.ToDateTime(new TimeOnly(0,0));
            
        if(!string.IsNullOrWhiteSpace(description))
            Description = description;

        CreatedAt = DateTime.UtcNow;
    }

    public int FinanceId { get; private set; }
    public int TenantId { get; private set; }
    public int FinanceTypeId { get; private set; }
    public int ServiceTypeId { get; private set; }
    public int CurrencyId { get; private set; }

    public decimal Amount { get; private set; }

    //public DateTime ServiceDate { get; private set; }
    public DateTime GivenDate { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? Deleted { get; private set; }

    public ServiceType ServiceType { get; private set; }
    public Currency Currency { get; private set; }
    public FinanceType FinanceType { get; private set; }
    public Tenant Tenant { get; private set; }


    public static Finance? Create(IValidateFinanceInDomain validator,
                                  Tenant tenant,
                                  decimal amount,
                                  FinanceEnum financeEnum,
                                  ServiceEnum serviceEnum,
                                  CurrencyEnum currencyEnum,
                                  DateOnly givenDate,
                                  string? description,
                                  out NotificationOutput notification)
    {
        notification = new NotificationOutput();

        if (!validator.Validate(amount,
                                givenDate,
                                out var errors))
        {
            notification.AddErrors(errors);
            return null;
        }
        
        return new(tenant, amount, financeEnum, serviceEnum, currencyEnum, givenDate, description);
    }
        

    public void Update(IValidateFinanceInDomain validator,
                       Tenant tenant,
                       decimal amount,
                       FinanceEnum financeEnum,
                       ServiceEnum serviceEnum,
                       CurrencyEnum currencyEnum,
                       DateOnly givenDate,
                       string? description,
                       out NotificationOutput notification)
    {
        notification = new NotificationOutput();

        if (!validator.Validate(amount,
                                givenDate,
                                out var errors))
        {
            notification.AddErrors(errors);
            return;
        }

        var serviceEnumValue = GetServiceTypeEnumValue(serviceEnum);
        var financeEnumValue = GetFinanceTypeEnumValue(financeEnum);
        var currencyEnumValue = GetCurrencyTypeEnumValue(currencyEnum);

        Tenant = tenant;
        TenantId = tenant.TenantId;
        FinanceTypeId = financeEnumValue.Id;
        ServiceTypeId = serviceEnumValue.Id;
        CurrencyId = currencyEnumValue.Id;
        Amount = amount;
        GivenDate = givenDate.ToDateTime(new TimeOnly(0,0));

        if (!string.IsNullOrWhiteSpace(description))
            Description = description;

        UpdatedAt = DateTime.UtcNow;
    }


    public void Delete() => Deleted = DateTime.UtcNow;

    private EnumValue GetServiceTypeEnumValue(ServiceEnum serviceEnum)
        => EnumService<ServiceEnum>.GetValue(serviceEnum);
        
    private EnumValue GetFinanceTypeEnumValue(FinanceEnum financeEnum)
        => EnumService<FinanceEnum>.GetValue(financeEnum);
        
    private EnumValue GetCurrencyTypeEnumValue(CurrencyEnum currencyEnum)
        => EnumService<CurrencyEnum>.GetValue(currencyEnum);
}