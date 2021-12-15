namespace Domain.Validators;

public class TenantInDomainValidator: IValidateTenantInDomain
{
    public void Validate(int currencyId,
                         IDictionary<string, object> errors)
    {
            
        if(currencyId <= 0)
            errors.Add(nameof(currencyId), "Invalid currency");
    }

    public void Validate(int currencyId, int tenantStatus,
                         IDictionary<string, object> errors)
    {

        if (currencyId <= 0)
            errors.Add(nameof(currencyId), "Invalid currency");

        if(tenantStatus <= 0)
            errors.Add(nameof(tenantStatus), "Invalid tenant status");
    }
}