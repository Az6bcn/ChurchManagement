using System;
using System.Collections.Generic;
using Domain.Entities.Helpers;
using Domain.Interfaces;
using Domain.Validators;
using Domain.ValueObjects;
using Shared.Enums;

namespace Domain.Entities.TenantAggregate
{
    public class Tenant : IEntity, IAggregateRoot
    {
        public Tenant()
        {
            _currencyCodes = new Dictionary<CurrencyEnum, string>
            {
                { CurrencyEnum.Naira, "NGN" },
                { CurrencyEnum.UsDollars, "USD" },
                { CurrencyEnum.BritishPounds, "GBP" }
            };
        }

        internal Tenant(string name,
                        string logoUrl,
                        CurrencyEnum currencyEnum,
                        IValidateTenantInDomain validator,
                        IDictionary<string, object> errors) : this()
        {
            var currencyEnumValue = GetCurrencyEnumValue(currencyEnum);
            Currency = Currency.Create(currencyEnumValue.Id, currencyEnumValue.Value);

            Name = name;
            LogoUrl = logoUrl;
            CreatedAt = DateTime.UtcNow;
            CurrencyId = Currency.CurrencyValueObjectId;
            TenantStatusId = (int) TenantStatusEnum.Pending;
            TenantGuidId = Guid.NewGuid();

            validator.Validate(CurrencyId, errors);
        }

        public int TenantId { get; private set; }
        public Guid TenantGuidId { get; private set; }
        public string Name { get; private set; }
        public string? LogoUrl { get; private set; }
        public int CurrencyId { get; private set; }
        public int TenantStatusId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }
        public Currency Currency { get; private set; }
        public TenantStatus TenantStatus { get; private set; }
        public string CurrencyCode => _currencyCodes[(CurrencyEnum)CurrencyId];
        private readonly IReadOnlyDictionary<CurrencyEnum, string> _currencyCodes;

        public static Tenant Create(string name,
                                    string logoUrl,
                                    CurrencyEnum currencyEnum,
                                    IValidateTenantInDomain validateTenantInDomain,
                                    out IDictionary<string, object> errors)
        {
            errors = new Dictionary<string, object>();
            return new Tenant(name, logoUrl, currencyEnum, validateTenantInDomain, errors);
        }

        public void Update(string name,
                                 string? logoUrl,
                                 CurrencyEnum currencyEnum,
                                 TenantStatusEnum tenantStatusEnum,
                                 IValidateTenantInDomain validator,
                                 out IDictionary<string, object> errors)
        {
            errors = new Dictionary<string, object>();

            var currencyEnumValue = GetCurrencyEnumValue(currencyEnum);
            var tenantStatusEnumValue = GetTenantStatusEnumValue(tenantStatusEnum);
            var tenantStatus
                = TenantStatus.Create(tenantStatusEnumValue.Id, tenantStatusEnumValue.Value);
            Currency = Currency.Create(currencyEnumValue.Id, currencyEnumValue.Value);
            
            validator.Validate(Currency.CurrencyValueObjectId, tenantStatus.TenantStatusId, errors);

            Name = name;
            LogoUrl = logoUrl;
            CurrencyId = Currency.CurrencyValueObjectId;
            TenantStatusId = tenantStatus.TenantStatusId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssignValuesToTenantStatusObject(TenantStatusEnum tenantStatusEnum)
        {
            var tenantStatusEnumValue = EnumService<TenantStatusEnum>.GetValue(tenantStatusEnum);
            TenantStatus = TenantStatus.Create(tenantStatusEnumValue.Id, tenantStatusEnumValue.Value);
        }

        public void UpdateStatus()
        {
        }

        public void UpdateCurrency()
        {
        }

        public void Delete() => Deleted = DateTime.UtcNow;


        private EnumValue GetCurrencyEnumValue(CurrencyEnum currencyEnum)
            => EnumService<CurrencyEnum>.GetValue(currencyEnum);

        private EnumValue GetTenantStatusEnumValue(TenantStatusEnum tenantStatusEnum)
            => EnumService<TenantStatusEnum>.GetValue(tenantStatusEnum);
    }
}