using Application.Dtos.Request.Create;
using Shared.Enums;

namespace Application.Dtos.Request.Update
{
    public class UpdateTenantRequestDto: CreateTenantRequestDto
    {
        public int TenantId { get; set; }
        public TenantStatusEnum TenantStatus { get; set; }
    }
}