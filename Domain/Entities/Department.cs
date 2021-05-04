﻿using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Department
    {
        private HashSet<Member> _members;
        private HashSet<DepartmentMembers> _departmentMembers;

        private Department()
        {
            _members = new();
            _departmentMembers = new();
        }
        
        public int DepartmentId { get; private set; }
        public int TenantId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get;private  set; }
        public DateTime? Deleted { get; private set; }

        public IReadOnlyCollection<Member> Members => _members;
        public IReadOnlyCollection<DepartmentMembers> DepartmentMembers => _departmentMembers;

    }


}