using System;

namespace Domain.ValueObjects
{
    public abstract class ValueObject
    {
        public string Currency { get; private set; }
        public int CurrencyId { get; set; }
        
        protected static bool Equals(ValueObject first, ValueObject second)
        {
            if (first is null || second is null)
                return false;

            return first.Currency.Equals(second.Currency, StringComparison.OrdinalIgnoreCase);
        }
    }
}