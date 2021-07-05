using System;
using System.Collections.Generic;
using Domain.Entities.Helpers;
using Domain.Interfaces;
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
                        TenantStatusEnum tenantStatusEnum) : this()
        {
            var currencyEnumValue = GetEnumValue<CurrencyEnum>(currencyEnum);
            var tenantStatusEnumValue = GetEnumValue<TenantStatusEnum>(tenantStatusEnum);
            Name = name;
            LogoUrl = logoUrl;
            CreatedAt = DateTime.UtcNow;
            Currency = Currency.Create(currencyEnumValue.Id, currencyEnumValue.Value);
            CurrencyId = Currency.CurrencyId;
            TenantStatusId = tenantStatusEnumValue.Id;
            TenantGuidId = Guid.NewGuid();
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

        public CurrencyEnum CurrencyEnum { get; set; }
        public TenantStatusEnum TenantStatusEnum { get; set; }
        public Currency Currency { get; private set; }
        public TenantStatus TenantStatus { get; private set; }

        public static Tenant Create(string name,
                                    string logoUrl,
                                    CurrencyEnum currencyEnum,
                                    TenantStatusEnum tenantStatusEnum)
            => new Tenant(name, logoUrl, currencyEnum, tenantStatusEnum);

        public void UpdateTenant(string name,
                                 string? logoUrl,
                                 CurrencyEnum CurrencyEnum)
        {
            Name = name;
            LogoUrl = logoUrl;
            CurrencyEnum = CurrencyEnum;
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

        internal EnumValue GetEnumValue<T>(T enumType) where T : Enum
        {
            EnumService<T>.GetEnumValue(enumType, out var result);

            return result;
        }

        public IEnumerable<string> Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return $"{nameof(Name)} is required to create a tenant";

            if (Currency is null) yield return $"{nameof(Currency)} is required to create a tenant";

            if (TenantStatus is null) yield return $"{nameof(Currency)} is required to create a tenant";
        }
    }
}