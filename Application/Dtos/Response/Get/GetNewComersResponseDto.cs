using Shared.Enums;

namespace Application.Dtos.Response.Get;

public class GetNewComersResponseDto
{
    public int NewComerId { get; set; }
    public ServiceEnum ServiceType { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string DateAndMonthOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateAttended { get; set; }
}