using Application.Dtos.Request.Update;
using Application.Dtos.Response.Update;

namespace Application.Commands.PersonManagements.Update;

public interface IUpdateNewComerCommand
{
    Task<UpdateNewComerResponseDto> ExecuteAsync(UpdateNewComerRequestDto request);
}