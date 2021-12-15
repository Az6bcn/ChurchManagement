namespace Application.Commands.PersonManagements.Delete;

public interface IDeleteMemberCommand
{
    Task ExecuteAsync(int memberId, int tenantId);
}