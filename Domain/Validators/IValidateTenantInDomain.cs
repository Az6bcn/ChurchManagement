namespace Domain.Validators;

public interface IValidateTenantInDomain
{
    void Validate(int currencyId,
                  IDictionary<string, object> errors);

    void Validate(int currencyId, int tenantStatus,
                  IDictionary<string, object> errors);
}