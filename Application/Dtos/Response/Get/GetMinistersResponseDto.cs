using Shared.Enums;

namespace Application.Dtos.Response.Get;

public class GetMinistersResponseDto
{
    public int MinisterId { get; set; }
    public int MemberId { get; set; }
    public int TenantId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string DateMonthOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public MinisterTitleEnum MinisterTitle { get; set; }
}