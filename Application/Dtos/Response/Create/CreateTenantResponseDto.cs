using Shared.Enums;

namespace Application.Dtos.Response.Create;

public class CreateTenantResponseDto
{
    public int TenantId { get; set; }
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
    public TenantStatusEnum TenantStatus { get; set; }
    public CurrencyEnum Currency { get; set; }
}