using Domain.Abstracts;

namespace Domain.ValueObjects
{
    public class FinanceType: ValueObject
    {
        private FinanceType()
        {

        }

        internal FinanceType(int id, string name)
        {
            FinanceTypeId = id;
            Name = name;
        }

        public int FinanceTypeId { get; private set; }
        public string Name { get; private set; }

        public static FinanceType Create(int id, string name) => new(id, name);
        protected override bool Equals(ValueObject value1, ValueObject value2)
        {
            throw new System.NotImplementedException();
        }
    }
}