using System.Threading.Tasks;
using Application.Dtos.Request.Create;

namespace Application.Commands.Finance.Create
{
    public interface ICreateFinanceCommand
    {
        Task ExecuteAsync(CreateFinanceRequestDto request);

    }
}