using System.Threading.Tasks;

namespace Application.Commands.Tenant.Delete
{
    public interface IDeleteTenantCommand
    {
        Task ExecuteAsync(int tenantId);
    }
}