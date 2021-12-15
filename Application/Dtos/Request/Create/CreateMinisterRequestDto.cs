using Shared.Enums;

namespace Application.Dtos.Request.Create;

public class CreateMinisterRequestDto
{
    public int MemberId { get; set; }
    public int TenantId { get; set; }
    public MinisterTitleEnum MinisterTitle { get; set; }
}