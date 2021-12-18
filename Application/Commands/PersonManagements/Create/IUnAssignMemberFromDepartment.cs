using Application.Dtos.Request.Create;

namespace Application.Commands.PersonManagements.Create;

public interface IUnAssignMemberFromDepartment
{
    Task ExecuteAsync(AssignMemberToDepartmentRequestDto request);
}