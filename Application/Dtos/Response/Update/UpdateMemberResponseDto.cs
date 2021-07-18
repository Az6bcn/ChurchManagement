namespace Application.Dtos.Response.Update
{
    public class UpdateMemberResponseDto
    {
        public int MemberId { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateAndMonthOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsWorker { get; set; }
    }
}