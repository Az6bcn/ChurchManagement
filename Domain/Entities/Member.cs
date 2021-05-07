using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Member
    {
        private readonly HashSet<Department> _departments;
        private readonly HashSet<DepartmentMembers> _departmentMembers;

        public Member()
        {
            _departments = new();
            _departmentMembers = new();
        }


        internal Member(int tenantId,
            string name,
            string surname,
            string dayMonthBirth,
            bool isWorker,
            string phoneNumber) : this()
        {
            TenantId = tenantId;
            Name = name;
            Surname = surname;
            DateAndMonthOfBirth = dayMonthBirth;
            IsWorker = isWorker;
            CreatedAt = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(phoneNumber))
                PhoneNumber = phoneNumber;
        }

        public int MemberId { get; private set; }
        public int TenantId { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string DateAndMonthOfBirth { get; private set; }
        public string Gender { get; private set; }
        public bool IsWorker { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }

        public Tenant Tenant { get; private set; }

        public IReadOnlyCollection<Department> Departments => _departments;
        public IReadOnlyCollection<DepartmentMembers> DepartmentMembers => _departmentMembers;

        public string FullName => $"{Name} {Surname}";

        public static Member CreateMember(int tenantId,
            string name,
            string surname,
            string dayMonthBirth,
            bool isWorker,
            string phoneNumber) => new Member(tenantId, name, surname, dayMonthBirth, isWorker, phoneNumber);
        
        public Member Create(int tenantId,
            string name,
            string surname,
            string dayMonthBirth,
            bool isWorker,
            string phoneNumber) => new Member(tenantId, name, surname, dayMonthBirth, isWorker, phoneNumber);


        public void UpdateMember(int tenantId,
            string name,
            string surname,
            string dayMonthBirth,
            bool isWorker,
            string phoneNumber)
        {
            TenantId = tenantId;
            Name = name;
            Surname = surname;
            DateAndMonthOfBirth = dayMonthBirth;
            IsWorker = isWorker;
            UpdatedAt = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(phoneNumber))
                PhoneNumber = phoneNumber;
        }

        public void DeleteMember() => Deleted = DateTime.UtcNow;
    }
}