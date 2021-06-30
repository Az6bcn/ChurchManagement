using System;
using System.Collections.Generic;
using Domain.Abstracts;
using Domain.AggregatesModel.MemberAggregate;
using Domain.AggregatesModel.TenantAggregate;
using Domain.Interfaces;

namespace Domain.AggregatesModel.DepartmentAggregate
{
    public class Department: Entity, IAggregateRoot
    {
        private HashSet<Member> _members;
        private HashSet<DepartmentMembers> _departmentMembers;

        private Department()
        {
            _members = new();
            _departmentMembers = new();
        }
        
        public int DepartmentId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get;private  set; }
        public DateTime? Deleted { get; private set; }

        public Tenant Tenant { get; private set; }
        public IReadOnlyCollection<Member> Members => _members;
        public IReadOnlyCollection<DepartmentMembers> DepartmentMembers => _departmentMembers;

    }


}