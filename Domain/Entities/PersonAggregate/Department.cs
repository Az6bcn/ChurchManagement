using System;
using System.Collections.Generic;
using Domain.Entities.TenantAggregate;
using Domain.Interfaces;

namespace Domain.Entities.PersonAggregate
{
    public class Department: IEntity
    {
        private HashSet<Member> _members;
        private HashSet<DepartmentMembers> _departmentMembers;

        private Department()
        {
            _members = new();
            _departmentMembers = new();
        }

        private Department(string name, Tenant tenant): this()
        {
            Name = name;
            TenantId = tenant.TenantId;
            Tenant = tenant;
        }
        
        public int DepartmentId { get; private set; }
        public int TenantId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get;private  set; }
        public DateTime? Deleted { get; private set; }

        public Tenant Tenant { get; private set; }
        public IReadOnlyCollection<Member> Members => _members;
        public IReadOnlyCollection<DepartmentMembers> DepartmentMembers => _departmentMembers;


        public static Department Create(string name,
                                  Tenant tenant) => new(name, tenant);

    }


}