using System.Threading.Tasks;
using Application.Dtos.Request.Create;

namespace Application.Commands.PersonManagement.Create
{
    public interface IUnAssignHeadOfDepartmentCommand
    {
        Task ExecuteAsync(AssignMemberToDepartmentRequestDto request);
    }
}