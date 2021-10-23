using System.Threading.Tasks;

namespace Application.Commands.PersonManagement.Delete
{
    public interface IDeleteNewComerCommand
    {
        Task ExecuteAsync(int newComerId, int tenantId);
    }
}