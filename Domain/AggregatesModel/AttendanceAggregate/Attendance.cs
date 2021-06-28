using System;
using Domain.Abstracts;

namespace Domain.Entities
{
    public class Attendance: Entity
    {
        public int AttendanceId { get; set; }
        public int TenantId { get; private set; }
        public DateTime ServiceDate { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Children { get; set; }
        public int NewComers { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }
        public Tenant Tenant { get; private set; }
    }
}