using Application.Dtos.Request.Create;

namespace Application.Commands.PersonManagements.Create;

public interface IAssignHeadOfDepartmentCommand
{
    Task ExecuteAsync(AssignMemberToDepartmentRequestDto request);
}