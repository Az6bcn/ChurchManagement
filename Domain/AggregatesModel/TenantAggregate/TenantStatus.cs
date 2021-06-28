using System.Collections.Generic;
using Domain.Abstracts;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class TenantStatus: Entity
    {
        private HashSet<Tenant> _tenants;
        public int TenantStatusId { get; private set; }
        public string Name { get; set; }

        public IReadOnlyCollection<Tenant> Tenants => _tenants;

        public TenantStatus()
        {
            _tenants = new HashSet<Tenant>();
        }

        internal TenantStatus(int id, string name)
        {
            TenantStatusId = id;
            Name = name;
        }
        
        internal TenantStatus(string name)
        {
            Name = name;
        }

        public static TenantStatus Create(string name) => new (name);
        public static TenantStatus Create(int id, string name) => new (id, name);
    }
}