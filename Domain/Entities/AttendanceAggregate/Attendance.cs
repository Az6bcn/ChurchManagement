using System;
using Domain.Abstracts;
using Domain.Entities.TenantAggregate;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities.AttendanceAggregate
{
    public class Attendance: IEntity, IAggregateRoot
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
        public ServiceType ServiceType { get; private set; }
    }
}