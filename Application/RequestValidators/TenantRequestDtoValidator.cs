using System.Collections.Generic;
using Application.Dtos.Request.Create;

namespace Application.RequestValidators
{
    public class TenantRequestDtoValidator : IValidateTenantRequestDto
    {
        public void Validate(CreateTenantRequestDto? request,
                             ICollection<string> tenantNames,
                             out IDictionary<string, object> errors)
        {
            errors = new Dictionary<string, object>();

            if (request is null)
                errors.Add(nameof(request), "Request cannot be null or empty");

            if (string.IsNullOrWhiteSpace(request?.Name))
                errors.Add(nameof(request.Name), "A tenant must have a name");

            if (tenantNames.Contains(request!.Name))
                errors.Add(nameof(request.Name), "A tenant with the name already exist");
        }
    }
}