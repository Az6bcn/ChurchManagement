using Application.Dtos.Request.Create;

namespace Application.Commands.PersonManagements.Create;

public interface IAssignMemberToDepartmentCommand
{
    Task ExecuteAsync(AssignMemberToDepartmentRequestDto request);
}