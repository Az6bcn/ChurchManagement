using System.Collections.Generic;
using System.Linq;
using Application.Dtos.Request.Create;
using Application.Dtos.Request.Update;
using Domain.Entities.PersonAggregate;

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
        
        public void ValidateDepartmentUpdate(UpdateDepartmentRequestDto request,
                                       ICollection<Department> departments,
                                       out IDictionary<string, object> errors)
        {
            errors = new Dictionary<string, object>();
            
            if(string.IsNullOrWhiteSpace(request.Name))
                errors.Add(nameof(request.Name), "Department name cannot be empty");
            
            if(!BelongsToTenant(departments!, request.DepartmentId))
                errors.Add(nameof(request.DepartmentId), "Update not permitted");

            if (departments.Any() && departments.Select(x => x.Name).Contains(request.Name))
                errors.Add(nameof(request.Name), "Department name already exist");
        }

        private bool BelongsToTenant(ICollection<Department> departments, int departmentId)
           => departments.Select(d => d.DepartmentId).Contains(departmentId);

    }
}