using System.Threading.Tasks;
using Application.Dtos.Request.Update;
using Application.Dtos.Response.Update;

namespace Application.Commands.PersonManagement.Update
{
    public interface IUpdateMinisterCommand
    {
        Task<UpdateMinisterResponseDto> ExecuteAsync(UpdateMinisterRequestDto request);
    }
}