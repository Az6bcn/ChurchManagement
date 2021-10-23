using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;

namespace Application.Commands.PersonManagement.Create
{
    public interface ICreateNewComerCommand
    {
        Task<CreateNewComerResponseDto> ExecuteAsync(CreateNewComerRequestDto request);
    }
}