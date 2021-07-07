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
        }

        internal Tenant(string name,
                        string logoUrl,
                        CurrencyEnum currencyEnum,
                        IValidateTenantCreation validator,
                        IDictionary<string, object> errors) : this()
        {
            var currencyEnumValue = GetCurrencyEnumValue(currencyEnum);
            Currency = Currency.Create(currencyEnumValue.Id, currencyEnumValue.Value);

            Name = name;
            LogoUrl = logoUrl;
            CreatedAt = DateTime.UtcNow;
            CurrencyId = Currency.CurrencyId;
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

        public static Tenant Create(string name,
                                    string logoUrl,
                                    CurrencyEnum currencyEnum,
                                    IValidateTenantCreation validateTenantCreation,
                                    out IDictionary<string, object> errors)
        {
            errors = new Dictionary<string, object>();
            return new Tenant(name, logoUrl, currencyEnum, validateTenantCreation, errors);
        }

        public void Update(string name,
                                 string? logoUrl,
                                 CurrencyEnum currencyEnum)
        {
            var currencyEnumValue = GetCurrencyEnumValue(currencyEnum);
            Name = name;
            LogoUrl = logoUrl;
            Currency = Currency.Create(currencyEnumValue.Id, currencyEnumValue.Value);
            CurrencyId = Currency.CurrencyId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssignValuesToTenantStatusObject(TenantStatusEnum tenantStatusEnum)
        {
            var tenantStatusEnumValue = GetEnumValue<TenantStatusEnum>(tenantStatusEnum);
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
            => GetEnumValue<CurrencyEnum>(currencyEnum);

        private EnumValue GetEnumValue<T>(T enumType) where T : Enum
        {
            EnumService<T>.GetEnumValue(enumType, out var result);

            return result;
        }
    }
}