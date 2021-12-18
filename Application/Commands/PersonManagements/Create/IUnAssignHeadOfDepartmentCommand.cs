using Application.Dtos.Request.Create;

namespace Application.Commands.PersonManagements.Create;

public interface IUnAssignHeadOfDepartmentCommand
{
    Task ExecuteAsync(AssignMemberToDepartmentRequestDto request);
}