using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;

namespace Application.Commands.Tenants.Create;

public interface ICreateTenantCommand
{
    Task<CreateTenantResponseDto> ExecuteAsync(CreateTenantRequestDto request);
}