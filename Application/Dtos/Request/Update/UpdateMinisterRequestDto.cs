using Shared.Enums;

namespace Application.Dtos.Request.Update;

public class UpdateMinisterRequestDto
{
    public int MinisterId { get; set; }
    public MinisterTitleEnum MinisterTitle { get; set; }
    public int TenantId { get; set; }
}