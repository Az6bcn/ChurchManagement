using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;

namespace Application.Commands.Tenant.Create
{
    public interface ICreateTenantCommand
    {
        Task<CreateTenantResponseDto> ExecuteAsync(CreateTenantRequestDto request);
    }
}