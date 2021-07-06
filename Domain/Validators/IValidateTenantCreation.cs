using System.Collections.Generic;
using Domain.Entities.TenantAggregate;
using Domain.ValueObjects;

namespace Domain.Validators
{
    public interface IValidateTenantCreation
    {
        void Validate(int currencyId,
                      IDictionary<string, object> errors);
    }
}