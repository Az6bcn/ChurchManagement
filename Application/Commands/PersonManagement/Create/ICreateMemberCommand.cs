using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;

namespace Application.Commands.PersonManagement.Create
{
    public interface ICreateMemberCommand
    {
        Task<CreateMemberResponseDto> ExecuteAsync(CreateMemberRequestDto request);
    }
}