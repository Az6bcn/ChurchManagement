using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application.Dtos.Request.Create;

namespace Application.RequestValidators
{
    public class PersonManagementRequestDtoValidator : IValidatePersonManagementRequestDto
    {
        public void ValidateDepartment(CreateDepartmentRequestDto request,
                                       ICollection<string?> departmentNames,
                                       out IDictionary<string, object> errors)
        {
            errors = new Dictionary<string, object>();
            
            if(string.IsNullOrWhiteSpace(request.Name))
                errors.Add(nameof(request.Name), "Department name cannot be empty");

            if (departmentNames.Any() && departmentNames.Contains(request.Name))
                errors.Add(nameof(request.Name), "Department name already exist");
                
        }
    }
}