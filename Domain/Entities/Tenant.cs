using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Tenant
    {
        private readonly HashSet<Finance> _finances;
        private readonly HashSet<Member> _members;
        private readonly HashSet<Attendance> _attendance;
        private readonly HashSet<NewComer> _newComers;
        private readonly HashSet<Minister> _ministers;

        private Tenant()
        {
            _finances = new();
            _members = new();
            _attendance = new ();
            _newComers = new ();
            _ministers = new ();
        }

        internal Tenant(string name, string logoUrl, int currencyId, int tenantStatus)
        {
            Name = name;
            LogoUrl = logoUrl;
            TenantStausId = tenantStatus;
            CurrencyId = currencyId;
            CreatedAt = DateTime.UtcNow;
        }

        public int TenantId { get; private set; }
        public Guid TenantGuidId { get; private set; }
        public string Name { get; private set; }
        public string? LogoUrl { get; private set; }
        public int CurrencyId { get; private set; }
        public int TenantStausId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }
        public Currency Currency { get; private set; }
        public TenantStatus TenantStatus { get; private set; }

        
        
        public IReadOnlyCollection<Finance> Finances => _finances;
        public IReadOnlyCollection<Member> Members => _members;
        public IReadOnlyCollection<Attendance> Attendance => _attendance;
        public IReadOnlyCollection<NewComer> NewComers => _newComers;
        public IReadOnlyCollection<Minister> Ministers => _ministers;
        
        public static Tenant CreateTenant(string name, string logoUrl, int currencyId, int tenantStatus)
            => new Tenant(name, logoUrl, currencyId, tenantStatus);

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