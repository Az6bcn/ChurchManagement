using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;

namespace Application.Commands.PersonManagements.Create;

public interface ICreateNewComerCommand
{
    Task<CreateNewComerResponseDto> ExecuteAsync(CreateNewComerRequestDto request);
}