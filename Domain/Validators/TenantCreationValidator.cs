using System.Collections.Generic;
using Domain.Entities.TenantAggregate;
using Domain.ValueObjects;

namespace Domain.Validators
{
    public class TenantCreationValidator: IValidateTenantCreation
    {
        public void Validate(int currencyId,
                                  IDictionary<string, object> errors)
        {
            
            if(currencyId <= 0)
                errors.Add(nameof(currencyId), "Invalid currency");
            
        }
    }
}