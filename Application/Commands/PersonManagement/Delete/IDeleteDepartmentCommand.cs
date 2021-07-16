using System.Threading.Tasks;

namespace Application.Commands.PersonManagement.Delete
{
    public interface IDeleteDepartmentCommand
    {
        Task ExecuteAsync(int departmentId, int tenantId);
    }
}