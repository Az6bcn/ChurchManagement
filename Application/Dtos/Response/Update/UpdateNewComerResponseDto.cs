using System;
using Shared.Enums;

namespace Application.Dtos.Response.Update
{
    public class UpdateNewComerResponseDto
    {
        public int NewComerId { get; set; }
        public int TenantId { get; set; }
        public ServiceEnum ServiceTypeEnum { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateAndMonthOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateAttended { get; set; }
    }
}