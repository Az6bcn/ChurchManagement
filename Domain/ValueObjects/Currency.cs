using System;
using Domain.Abstracts;

namespace Domain.ValueObjects
{
    public class Currency: ValueObject
    {
        public Currency()
        {
            
        }
        
        internal Currency(int id, string name)
        {
            CurrencyId = id;
            Name = name;
        }
        
        
        public int CurrencyId { get; private set; }
        public string Name { get; private set; }
        public int CurrencyValueObjectId => CurrencyId;
        
        
        public static Currency Create(int id, string name) => new Currency(id, name);
        public static Currency Update(int id, string name) => new Currency(id, name);

        
        protected override bool Equals(ValueObject value1, ValueObject value2)
        {
            throw new NotImplementedException();
        }
    }
}