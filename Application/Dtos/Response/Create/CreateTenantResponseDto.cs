using Application.Enums;

namespace Application.Dtos.Response.Create
{
    public class CreateTenantResponseDto
    {
        public string Name { get; set; }
        public string? LogoUrl { get; set; }
        public TenantStatusEnum TenantStatusEnum { get; set; }
        public CurrencyEnum CurrencyEnum { get; set; }
    }
}