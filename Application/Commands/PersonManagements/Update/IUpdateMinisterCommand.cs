using Application.Dtos.Request.Update;
using Application.Dtos.Response.Update;

namespace Application.Commands.PersonManagements.Update;

public interface IUpdateMinisterCommand
{
    Task<UpdateMinisterResponseDto> ExecuteAsync(UpdateMinisterRequestDto request);
}