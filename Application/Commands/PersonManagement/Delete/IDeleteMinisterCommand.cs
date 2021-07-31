using System.Threading.Tasks;

namespace Application.Commands.PersonManagement.Delete
{
    public interface IDeleteMinisterCommand
    {
        Task ExecuteAsync(int memberId, int tenantId);
    }
}