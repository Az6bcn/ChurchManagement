namespace Application.Commands.PersonManagements.Delete;

public interface IDeleteDepartmentCommand
{
    Task ExecuteAsync(int departmentId, int tenantId);
}