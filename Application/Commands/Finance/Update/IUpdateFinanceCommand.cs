using System.Threading.Tasks;
using Application.Dtos.Request.Update;

namespace Application.Commands.Finance.Update
{
    public interface IUpdateFinanceCommand
    {
        Task ExecuteAsync(UpdateFinanceRequestDto request);
    }
}