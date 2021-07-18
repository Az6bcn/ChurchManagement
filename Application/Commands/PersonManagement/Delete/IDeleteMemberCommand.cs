using System.Threading.Tasks;

namespace Application.Commands.PersonManagement.Delete
{
    public interface IDeleteMemberCommand
    {
        Task ExecuteAsync(int memberId, int tenantId);
    }
}