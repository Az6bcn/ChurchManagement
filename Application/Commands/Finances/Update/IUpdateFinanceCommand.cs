using Application.Dtos.Request.Update;

namespace Application.Commands.Finances.Update;

public interface IUpdateFinanceCommand
{
    Task ExecuteAsync(UpdateFinanceRequestDto request);
}