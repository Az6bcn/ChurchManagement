using Domain.Abstracts;

namespace Domain.Entities.FinanceAggregate
{
    public class Currency: Entity
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

        public static Currency CreateCurrency(string name) => new(name);
        public static Currency CreateCurrency(int id, string name) => new(id, name);
    }
}