using System.Threading.Tasks;
using Application.Dtos.Request.Update;
using Application.Dtos.Response.Update;

namespace Application.Commands.Tenant.Update
{
    public interface IUpdateTenantCommand
    {
        Task<UpdateTenantResponseDto> ExecuteAsync(UpdateTenantRequestDto request);
    }
}