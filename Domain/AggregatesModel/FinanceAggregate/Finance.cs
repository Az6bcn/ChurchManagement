using System;
using Domain.Abstracts;

namespace Domain.Entities
{
    public class Finance: Entity
    {
        private Finance()
        {
        }

        public int FinanceId { get; private set; }
        public int TenantId { get; private set; }
        public int FinanceTypeId { get; private set; }
        public int ServiceTypeId { get; private set; }
        public int CurrencyId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime? ServiceDate { get; private set; }
        public DateTime? GivenDate { get; private set; }
        public string? Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }

        public ServiceType ServiceType { get; private set; }
        public Currency Currency { get; private set; }
        public FinanceType FinanceType { get; private set; }
        public Tenant Tenant { get; private set; }
    }
}
