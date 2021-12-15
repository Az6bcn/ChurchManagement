namespace Application.Dtos.Request.Create;

public class CreateMemberRequestDto
{
    public int TenantId { get; set; } = default;
    public string Name { get; set; }
    public string Surname { get; set; }
    public string DateAndMonthOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsWorker { get; set; }
}