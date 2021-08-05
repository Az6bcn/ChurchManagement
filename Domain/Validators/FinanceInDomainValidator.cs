using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.FinanceAggregate;

namespace Domain.Validators
{
    public class FinanceInDomainValidator : IValidateFinanceInDomain
    {
        public bool Validate(decimal amount,
                             DateTime givenDate,
                             out Dictionary<string, object> errors)
        {
            errors = new Dictionary<string, object>();
            
            if(amount < 0)
                errors.Add(nameof(amount), "Amount cannot be less than zero");
            
            if(givenDate == new DateTime())
                errors.Add(nameof(givenDate), "Date is required");

            return !errors.Any();
        }
    }
}