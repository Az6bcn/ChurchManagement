using Application.Dtos.Request.Update;
using Application.Dtos.Response.Update;

namespace Application.Commands.Tenants.Update;

public interface IUpdateTenantCommand
{
    Task<UpdateTenantResponseDto> ExecuteAsync(UpdateTenantRequestDto request);
}