using System;
using Domain.Abstracts;

namespace Domain.Entities
{
    public class Minister: Entity
    {
        private Minister()
        {
            
        }
        
        internal Minister(int tenantId,
            string name,
            string surname,
            string dayMonthBirth,
            string phoneNumber,
            int ministerTitleId,
            int serviceTypeId) : base()
        {
        }

        public int MinisterId { get; private set; }
        public int MemberId { get; private set; }
        public int MinisterTitleId { get; private set; }
        public new int TenantId { get; private set; }
        public new Tenant Tenant { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }
        public MinisterTitle MinisterTitle { get; private set; }
        public Member Member { get; private set; }

        public string Title => MinisterTitle.Name;


    }
}