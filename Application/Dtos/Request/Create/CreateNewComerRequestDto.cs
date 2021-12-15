using Shared.Enums;

namespace Application.Dtos.Request.Create;

public class CreateNewComerRequestDto
{
    public int TenantId { get; set; }
    public ServiceEnum ServiceTypeEnum { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string DateAndMonthOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateAttended { get; set; }
}