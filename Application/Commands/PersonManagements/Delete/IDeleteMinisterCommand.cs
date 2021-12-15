namespace Application.Commands.PersonManagements.Delete;

public interface IDeleteMinisterCommand
{
    Task ExecuteAsync(int memberId, int tenantId);
}