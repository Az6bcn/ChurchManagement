using System;
using System.Collections.Generic;
using Domain.Abstracts;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Tenant: Entity
    {
        public Tenant()
        {
            
        }

        internal Tenant(string name, string logoUrl, int currencyId, int tenantStatus): this()
        {
            Name = name;
            LogoUrl = logoUrl;
            TenantStatusId = tenantStatus;
            CurrencyId = currencyId;
            CreatedAt = DateTime.UtcNow;
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
       
       
        public static Tenant Create(
            string name,
            string logoUrl,
            int currencyId,
            int tenantStatusId
        ) => new Tenant(name, logoUrl, currencyId, tenantStatusId);
        
        public void UpdateTenant(string name, string? logoUrl, int currencyId)
        {
            Name = name;
            LogoUrl = logoUrl;
            CurrencyId = currencyId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus()
        {
            
        }

        public void UpdateCurrency()
        {
            
        }

        public void Delete() => Deleted = DateTime.UtcNow;

        public IEnumerable<string> Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return $"{nameof(Name)} is required to create a tenant";

            if (Currency is null)
                yield return $"{nameof(Currency)} is required to create a tenant";
            
            if (TenantStatus is null)
                yield return $"{nameof(Currency)} is required to create a tenant";
        } 
    }
}