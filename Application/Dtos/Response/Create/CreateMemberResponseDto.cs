using System;

namespace Application.Dtos.Response.Create
{
    public class CreateMemberResponseDto
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateAndMonthOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsWorker { get; set; }
        public DateTime DateAttended { get; private set; }
        public int ServiceTypeId { get; private set; }
    }
}