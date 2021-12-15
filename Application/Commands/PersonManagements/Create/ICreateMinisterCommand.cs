using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;

namespace Application.Commands.PersonManagements.Create;

public interface ICreateMinisterCommand
{
    Task<CreateMinisterResponseDto> ExecuteAsync(CreateMinisterRequestDto request);
}