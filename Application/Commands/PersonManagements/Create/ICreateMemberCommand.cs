using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;

namespace Application.Commands.PersonManagements.Create;

public interface ICreateMemberCommand
{
    Task<CreateMemberResponseDto> ExecuteAsync(CreateMemberRequestDto request);
}