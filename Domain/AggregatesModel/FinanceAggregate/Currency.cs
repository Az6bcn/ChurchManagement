using System.Collections.Generic;
using Domain.Abstracts;

namespace Domain.Entities
{
    public class Currency: Entity
    {
        private HashSet<Tenant> _tenants;
        private Currency()
        {
            _tenants = new HashSet<Tenant>();
        }

        internal Currency(string name)
        {
            Name = name;
        }
        internal Currency(int id, string name)
        {
            CurrencyId = id;
            Name = name;
        }

        public int CurrencyId { get; private set; }
        public string Name { get; private set; }

        public IReadOnlyCollection<Tenant> Tenants => _tenants;

        public static Currency CreateCurrency(string name) => new(name);
        public static Currency CreateCurrency(int id, string name) => new(id, name);
    }
}