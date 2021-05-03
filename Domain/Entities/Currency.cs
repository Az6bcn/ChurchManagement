using System.Collections.Generic;

namespace Domain.Entities
{
    public class Currency
    {
        private Currency()
        {

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

        public Tenant Tenant { get; private set; }

        public static Currency CreateCurrency(string name) => new(name);
        public static Currency CreateCurrency(int id, string name) => new(id, name);
    }
}