using System.Threading.Tasks;
using Application.Dtos.Request.Create;

namespace Application.Commands.PersonManagement.Create
{
    public interface IAssignMemberToDepartmentCommand
    {
        Task ExecuteAsync(AssignMemberToDepartmentRequestDto request);
    }
}