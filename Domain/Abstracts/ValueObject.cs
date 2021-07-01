using System;

namespace Domain.Abstracts
{
    // https://enterprisecraftsmanship.com/posts/entity-vs-value-object-the-ultimate-list-of-differences/
    public abstract class ValueObject
    {
        protected abstract bool Equals(ValueObject value1, ValueObject value2);

        protected bool StringValuesEquals(string value1, string value2)
        {
            if (string.IsNullOrWhiteSpace(value1) || string.IsNullOrWhiteSpace(value2))
                return false;

            return value1.Equals(value2, StringComparison.OrdinalIgnoreCase);
        }
    }
}