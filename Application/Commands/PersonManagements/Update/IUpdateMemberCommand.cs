using Application.Dtos.Request.Update;
using Application.Dtos.Response.Update;

namespace Application.Commands.PersonManagements.Update;

public interface IUpdateMemberCommand
{
    Task<UpdateMemberResponseDto> ExecuteAsync(UpdateMemberRequestDto request);
}