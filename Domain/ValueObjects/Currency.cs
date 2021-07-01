using System;
using Domain.Abstracts;

namespace Domain.ValueObjects
{
    public class Currency: ValueObject
    {
        internal Currency(string name, int id)
        {
            CurrencyId = id;
            Name = name;
        }
        
        public int CurrencyId { get; private set; }
        public string Name { get; private set; }
        
        
        public Currency Create(string name, int id) => new Currency(name, id);
        public Currency Update(string name, int id) => new Currency(name, id);
        
        protected override bool Equals(ValueObject value1, ValueObject value2)
        {
            throw new NotImplementedException();
        }
    }
}