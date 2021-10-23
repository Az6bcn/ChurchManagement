using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Validators
{
    public class FinanceInDomainValidator : IValidateFinanceInDomain
    {
        public bool Validate(decimal amount,
                             DateOnly givenDate,
                             out Dictionary<string, object> errors)
        {
            errors = new Dictionary<string, object>();
            
            if(amount < 0)
                errors.Add(nameof(amount), "Amount cannot be less than zero");
            
            if(givenDate == new DateOnly())
                errors.Add(nameof(givenDate), "Date is required");

            return !errors.Any();
        }
    }
}