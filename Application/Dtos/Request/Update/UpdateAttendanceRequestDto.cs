using System;
using Shared.Enums;

namespace Application.Dtos.Request.Update
{
    public class UpdateAttendanceRequestDto
    {
        public int AttendanceId { get; set; }
        public int TenantId { get;  set; }
        public DateTime ServiceDate { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Children { get; set; }
        public int NewComers { get; set; }
        public ServiceEnum ServiceTypeEnum { get; set; }
    }
}