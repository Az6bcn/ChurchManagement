using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;

namespace Application.Commands.PersonManagements.Create;

public interface ICreateDepartmentCommand
{
    Task<CreateDepartmentResponseDto> ExecuteAsync(int tenantId, CreateDepartmentRequestDto request);
}