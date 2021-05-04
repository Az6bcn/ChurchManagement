using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Tenant
    {
        private readonly HashSet<Finance> _finances;
        private readonly HashSet<Member> _members;

        private Tenant()
        {
            _finances = new();
            _members = new();
        }

        internal Tenant(string name, string logoUrl, int currencyId)
        {
            Name = name;
            LogoUrl = logoUrl;
            IsActive = true;
            CurrencyId = currencyId;
            CreatedAt = DateTime.UtcNow;
        }

        public int TenantId { get; private set; }
        public Guid TenantGuidId { get; private set; }
        public string Name { get; private set; }
        public string? LogoUrl { get; private set; }
        public int CurrencyId { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }

        public IReadOnlyCollection<Finance> Finances => _finances;
        public IReadOnlyCollection<Member> Members => _members;

        public Currency Currency { get; private set; }

        public static Tenant CreateTenant(string name, string logoUrl, int currencyId)
            => new Tenant(name, logoUrl, currencyId);

        public void Delete() => Deleted = DateTime.UtcNow;

        public void UpdateTenant(string name, string? logoUrl, int currencyId)
        {
            Name = name;
            LogoUrl = logoUrl;
            CurrencyId = currencyId;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}