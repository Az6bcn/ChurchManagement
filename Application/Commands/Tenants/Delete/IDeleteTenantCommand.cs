namespace Application.Commands.Tenants.Delete;

public interface IDeleteTenantCommand
{
    Task ExecuteAsync(int tenantId);
}