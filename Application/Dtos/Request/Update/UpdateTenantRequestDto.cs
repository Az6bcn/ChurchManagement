using Application.Dtos.Request.Create;
using Shared.Enums;

namespace Application.Dtos.Request.Update
{
    public class UpdateTenantRequestDto: CreateTenantRequestDto
    {
        public int Id { get; set; }
    }
}