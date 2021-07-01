using Domain.Abstracts;

namespace Domain.ValueObjects
{
    public class ServiceType: ValueObject
    {
        internal ServiceType()
        {
        }

        internal ServiceType(int id, string name)
        {
            ServiceTypeId = id;
            Name = name;
        }

        public int ServiceTypeId { get; private set; }
        public string Name { get; private set; }

        public static ServiceType Create(int id, string name) => new(id, name);
        public static ServiceType Update(int id, string name) => new(id, name);
        
        protected override bool Equals(ValueObject value1, ValueObject value2)
        {
            throw new System.NotImplementedException();
        }
    }
}