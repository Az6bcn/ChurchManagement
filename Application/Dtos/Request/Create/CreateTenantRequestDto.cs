using Application.Enums;

namespace Application.Dtos.Request.Create
{
    public class CreateTenantRequestDto
    {
        public string Name { get; set; }
        public string? LogoUrl { get; set; }
        public TenantStatusEnum TenantStatusEnum { get; set; }
    }
}