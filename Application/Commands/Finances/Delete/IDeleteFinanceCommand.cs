namespace Application.Commands.Finances.Delete;

public interface IDeleteFinanceCommand
{
    Task ExecuteAsync(int financeId, int tenantId);
}