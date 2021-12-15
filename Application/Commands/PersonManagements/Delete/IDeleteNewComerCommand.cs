namespace Application.Commands.PersonManagements.Delete;

public interface IDeleteNewComerCommand
{
    Task ExecuteAsync(int newComerId, int tenantId);
}