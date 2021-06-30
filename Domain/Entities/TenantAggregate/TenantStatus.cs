using System.Collections.Generic;
using Domain.Abstracts;

namespace Domain.AggregatesModel.TenantAggregate
{
    public class TenantStatus: Entity
    {
        public int TenantStatusId { get; private set; }
        public string Name { get; set; }

        public TenantStatus()
        {
        }

        internal TenantStatus(int id, string name)
        {
            TenantStatusId = id;
            Name = name;
        }
        
        internal TenantStatus(string name)
        {
            Name = name;
        }

        public static TenantStatus Create(string name) => new (name);
        public static TenantStatus Create(int id, string name) => new (id, name);
    }
}