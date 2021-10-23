using System;
using Shared.Enums;

namespace Application.Dtos.Request.Create
{
    public class CreateAttendanceRequestDto
    {
        public int TenantId { get;  set; }
        public DateOnly ServiceDate { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Children { get; set; }
        public int NewComers { get; set; }
        public ServiceEnum ServiceTypeEnum { get; set; }
    }
}