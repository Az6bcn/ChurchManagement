using Application.Dtos.Request.Create;
using System.Threading.Tasks;

namespace Application.Commands.PersonManagement.Create
{
    public interface IAssignHeadOfDepartmentCommand
    {
        Task ExecuteAsync(AssignMemberToDepartmentRequestDto request);
    }
}
