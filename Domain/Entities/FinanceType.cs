namespace Domain.Entities
{
    public class FinanceType
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

        public static FinanceType CreateFinanceType(string name) => new(name);
        public static FinanceType CreateFinanceType(int id, string name) => new(id, name);
    }
}