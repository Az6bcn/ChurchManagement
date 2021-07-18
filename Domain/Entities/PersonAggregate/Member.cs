using System;
using System.Collections.Generic;
using Domain.Entities.TenantAggregate;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities.PersonAggregate
{
    public class Member : IEntity
    {
        private readonly HashSet<Department> _departments;
        private readonly HashSet<DepartmentMembers> _departmentMembers;

        public Member()
        {
            _departmentMembers = new();
            _departments = new();
        }


        internal Member(Person person,
                        Tenant tenant,
                        bool isWorker = false) : this()
        {
            Tenant = tenant;
            TenantId = tenant.TenantId;
            Name = person.Name;
            Surname = person.Surname;
            DateMonthOfBirth = person.DateAndMonthOfBirth;
            IsWorker = isWorker;
            PhoneNumber = person.PhoneNumber;
            Gender = person.Gender;
            CreatedAt = DateTime.UtcNow;
        }

        public int MemberId { get; private set; }
        public bool IsWorker { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }
        public int TenantId { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string DateMonthOfBirth { get; private set; }
        public string Gender { get; private set; }
        public string PhoneNumber { get; private set; }
        public IReadOnlyCollection<Department> Departments => _departments;
        public IReadOnlyCollection<DepartmentMembers> DepartmentMembers => _departmentMembers;

        public Minister Minister { get; set; }
        public Person Person { get; private set; }
        public Tenant Tenant { get; private set; }

        public string PersonFullName => Person.FullName;
        public string FullName => $"{Name} {Surname}";

        internal static Member Create(Person person,
                                    Tenant tenant,
                                    bool isWorker) 
            => new Member(person, tenant, isWorker);


        internal void UpdateMember(Person person,
                                 bool isWorker)
        {
            TenantId = person.TenantId;
            Name = person.Name;
            Surname = person.Surname;
            DateMonthOfBirth = person.DateAndMonthOfBirth;
            IsWorker = isWorker;
            PhoneNumber = person.PhoneNumber;
            UpdatedAt = DateTime.UtcNow;
        }

        internal void Delete() => Deleted = DateTime.UtcNow;
    }
}