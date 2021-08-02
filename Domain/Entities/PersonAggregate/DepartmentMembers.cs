using System;
using System.Collections.Generic;
using Domain.Interfaces;
using Domain.Validators;

namespace Domain.Entities.PersonAggregate
{
    public class DepartmentMembers : IEntity
    {
        private DepartmentMembers()
        {
        }
        
        public DepartmentMembers(int departmentId,
                                   int memberId,
                                   bool isHod,
                                   DateTime dateJoined)
        {
            DepartmentId = departmentId;
            MemberId = memberId;
            DateJoined = dateJoined;
            CreatedAt = DateTime.UtcNow;
        }

        internal DepartmentMembers(Department department,
                                   Member member,
                                   bool isHod,
                                   DateTime dateJoined)
        {
            if (!Validate(dateJoined, out IDictionary<string, object> error))
                throw new DomainValidationException("Failed validation", error);

            DepartmentId = department.DepartmentId;
            MemberId = member.MemberId;
            Member = member;
            Department = department;
            IsHeadOfDepartment = isHod;
            DateJoined = dateJoined;
            CreatedAt = DateTime.UtcNow;
            Deleted = null;
        }

        public int DepartmentId { get; private set; }
        public int MemberId { get; set; }
        public bool IsHeadOfDepartment { get; private set; }
        public DateTime DateJoined { get; private set; }
        public DateTime? DateLeft { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }

        public Department Department { get; private set; }
        public Member Member { get; private set; }

        public static DepartmentMembers Assign(Department department,
                                               Member member,
                                               bool isHod,
                                               DateTime dateJoined)
            => new(department, member, isHod, dateJoined);

        public void UnAssignMember() => Deleted = DateTime.UtcNow;

        internal void AssignAsHod() => IsHeadOfDepartment = true;

        public void RemoveAsHod() => IsHeadOfDepartment = false;


        private bool Validate(DateTime dateJoined,
                              out IDictionary<string, object> error)
        {
            error = new Dictionary<string, object>();
            
            if (dateJoined == new DateTime())
            {
                error.Add(nameof(dateJoined), "Invalid date value for date joined");

                return false;
            }

            return true;
        }
    }
}