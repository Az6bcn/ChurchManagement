using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;

namespace Application.Commands.Tenant.Create
{
    public class TenantCommandCreator: ICreateTenantCommand
    {
        public TenantCommandCreator()
        {
            
        }

        public async Task<CreateTenantResponseDto> ExecuteAsync(CreateTenantRequestDto request)
        {
            return null;
        }
    }
}