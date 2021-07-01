using Domain.Abstracts;
using Domain.Interfaces;

namespace Domain.Entities.FinanceAggregate
{
    public class FinanceType: IEntity
    {
        private FinanceType()
        {

        }

        internal FinanceType(string name)
        {
            Name = name;
        }

        internal FinanceType(int id, string name)
        {
            FinanceTypeId = id;
            Name = name;
        }

        public int FinanceTypeId { get; private set; }
        public string Name { get; private set; }

        public static FinanceType Create(string name) => new(name);
        public static FinanceType Create(int id, string name) => new(id, name);
    }
}