using System;
using System.Collections.Generic;
using Domain.Abstracts;
using Shared.Enums;

namespace Domain.ValueObjects
{
    public class Currency: ValueObject
    {
        private readonly IReadOnlyDictionary<CurrencyEnum, string> _currencyCodes;
        public Currency()
        {
            _currencyCodes = new Dictionary<CurrencyEnum, string>
            {
                { CurrencyEnum.Naira, "NGN" },
                { CurrencyEnum.UsDollars, "USD" },
                { CurrencyEnum.BritishPounds, "GBP" }
            };
        }
        
        internal Currency(int id, string name)
        {
            CurrencyId = id;
            Name = name;
        }
        
        
        private int CurrencyId { get; init ; }
        private string Name { get; init; }
        public string CurrencyCode => _currencyCodes[(CurrencyEnum)CurrencyId];
        public int CurrencyValueObjectId => CurrencyId;
        
        
        public static Currency Create(int id, string name) => new Currency(id, name);
        public static Currency Update(int id, string name) => new Currency(id, name);

        
        protected override bool Equals(ValueObject value1, ValueObject value2)
        {
            throw new NotImplementedException();
        }
    }
}