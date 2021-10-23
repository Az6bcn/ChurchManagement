using System.Threading.Tasks;
using Application.Dtos.Request.Create;

namespace Application.Commands.PersonManagement.Create
{
    public interface IUnAssignMemberFromDepartment
    {
        Task ExecuteAsync(AssignMemberToDepartmentRequestDto request);
    }
}