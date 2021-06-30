using System;
using Domain.Abstracts;
using Domain.AggregatesModel.ServiceTypeAggregate;
using Domain.AggregatesModel.TenantAggregate;
using Domain.Interfaces;

namespace Domain.AggregatesModel.AttendanceAggregate
{
    public class Attendance: Entity, IAggregateRoot
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