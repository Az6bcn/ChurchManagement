using System;
using System.Collections.Generic;
using Domain.Abstracts;
using Domain.Entities.DepartmentAggregate;
using Domain.Entities.TenantAggregate;
using Domain.Interfaces;

namespace Domain.Entities.MemberAggregate
{
    public class Member: IEntity, IAggregateRoot
    {
        private readonly HashSet<Department> _departments;
        private readonly HashSet<DepartmentMembers> _departmentMembers;

        public Member()
        {
            _departments = new();
            _departmentMembers = new();
        }


        internal Member(
            string name,
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
        public Minister Minister { get; set; }

        public Person Person { get; private set; }
        public Tenant Tenant { get; private set; }
        public IReadOnlyCollection<Department> Departments => _departments;

        public string FullName => $"{Name} {Surname}";
        
        public static Member Create(
            string name,
            string surname,
            string dayMonthBirth,
            bool isWorker,
            string phoneNumber,
            Tenant tenant) => new Member(name, surname, dayMonthBirth, isWorker, phoneNumber, tenant);


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

        public IEnumerable<string> Validate()
        {
            if (Tenant is null)
                yield return $"{nameof(Tenant)} is required to create a member";
            
            if (string.IsNullOrWhiteSpace(Name))
                yield return $"{nameof(Name)} is required to create a member";
            
            if (string.IsNullOrWhiteSpace(Surname))
                yield return $"{nameof(Surname)} is required to create a member";
            
            if (string.IsNullOrWhiteSpace(Gender))
                yield return $"{nameof(Gender)} is required to create a member";
            
            if (string.IsNullOrWhiteSpace(DateAndMonthOfBirth))
                yield return $"{nameof(DateAndMonthOfBirth)} is required to create a member";
            
            if (!string.IsNullOrWhiteSpace(PhoneNumber))
                yield return $"{nameof(PhoneNumber)} is required to create a member";
        }
    }
}