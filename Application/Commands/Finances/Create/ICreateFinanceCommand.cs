using Application.Dtos.Request.Create;

namespace Application.Commands.Finances.Create;

public interface ICreateFinanceCommand
{
    Task ExecuteAsync(CreateFinanceRequestDto request);

}