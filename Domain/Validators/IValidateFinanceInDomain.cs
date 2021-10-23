using System;
using System.Collections.Generic;

namespace Domain.Validators
{
    public interface IValidateFinanceInDomain
    {
        bool Validate(decimal amount,
                      DateOnly givenDate,
                      out Dictionary<string, object> errors);
    }
}