using System;
using Domain.Entities.TenantAggregate;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities.PersonAggregate
{
    public class Minister : IEntity
    {
        private Minister()
        {
        }

        internal Minister(int tenantId,
                          string name,
                          string surname,
                          string dayMonthBirth,
                          string phoneNumber,
                          int ministerTitleId)
        {
        }

        public int MinisterId { get; private set; }
        public int MemberId { get; private set; }
        public int MinisterTitleId { get; private set; }
        public int TenantId { get; private set; }
        public Tenant Tenant { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }
        public MinisterTitle MinisterTitle { get; private set; }
        
        public Member Member { get; private set; }

        public string Title => MinisterTitle.Name;
    }
}