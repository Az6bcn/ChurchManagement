using System.Collections.Generic;
using Application.Dtos.Request.Create;

namespace Application.RequestValidators
{
    public interface IValidatePersonManagementRequestDto
    {
        void ValidateDepartment(CreateDepartmentRequestDto request,
                                ICollection<string?> departmentNames,
                                out IDictionary<string, object> errors);
    }
}