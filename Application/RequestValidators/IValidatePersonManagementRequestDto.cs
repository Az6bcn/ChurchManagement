using Application.Dtos.Request.Create;
using Application.Dtos.Request.Update;
using Domain.Entities.PersonAggregate;

namespace Application.RequestValidators;

public interface IValidatePersonManagementRequestDto
{
    void ValidateDepartment(CreateDepartmentRequestDto request,
                            ICollection<string?> departmentNames,
                            out IDictionary<string, object> errors);

    void ValidateDepartmentUpdate(UpdateDepartmentRequestDto request,
                                  ICollection<Department> departments,
                                  out IDictionary<string, object> errors);
        
}