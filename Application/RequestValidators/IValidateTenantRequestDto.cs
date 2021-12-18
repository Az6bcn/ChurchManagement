using Application.Dtos.Request.Create;

namespace Application.RequestValidators;

public interface IValidateTenantRequestDto
{
    void Validate(CreateTenantRequestDto? request,
                  ICollection<string> tenantNames,
                  out IDictionary<string, object> errors);
}