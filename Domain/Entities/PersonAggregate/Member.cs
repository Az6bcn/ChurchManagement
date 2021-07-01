using System;
using System.Collections.Generic;
using Domain.Entities.DepartmentAggregate;
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
            _departmentMembers = new ();
            _departments = new();
        }


        internal Member(string name,
                        string surname,
                        string dayMonthBirth,
                        bool isWorker,
                        string phoneNumber,
                        Tenant tenant) : this()
        {
            Tenant = tenant;
            // TenantId = tenant.TenantId;
            // Name = name;
            // Surname = surname;
            // DateAndMonthOfBirth = dayMonthBirth;
            IsWorker = isWorker;
            //PhoneNumber = phoneNumber;
            CreatedAt = DateTime.UtcNow;
        }

        public int MemberId { get; private set; }

        // public int TenantId { get; private set; }
        // public string Name { get; private set; }
        // public string Surname { get; private set; }
        // public string DateAndMonthOfBirth { get; private set; }
        // public string Gender { get; private set; }
        // public string PhoneNumber { get; private set; }
        public bool IsWorker { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }
        public IReadOnlyCollection<Department> Departments => _departments;
        public IReadOnlyCollection<DepartmentMembers> DepartmentMembers => _departmentMembers;

        public Minister Minister { get; set; }
        public Person Person { get; private set; }
        public Tenant Tenant { get; private set; }
        
        public string FullName => Person.FullName;

        public static Member Create(string name,
                                    string surname,
                                    string dayMonthBirth,
                                    bool isWorker,
                                    string phoneNumber,
                                    Tenant tenant) =>
            new Member(name, surname, dayMonthBirth, isWorker, phoneNumber, tenant);


        public void UpdateMember(int tenantId,
                                 string name,
                                 string surname,
                                 string dayMonthBirth,
                                 bool isWorker,
                                 string phoneNumber)
        {
            // TenantId = tenantId;
            // Name = name;
            // Surname = surname;
            // DateAndMonthOfBirth = dayMonthBirth;
            IsWorker = isWorker;
            UpdatedAt = DateTime.UtcNow;

            //if (!string.IsNullOrWhiteSpace(phoneNumber))
            //PhoneNumber = phoneNumber;
        }

        public void DeleteMember() => Deleted = DateTime.UtcNow;
    }
}