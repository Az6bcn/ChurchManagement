using System;
using Domain.Interfaces;

namespace Domain.Entities.PersonAggregate
{
    public class DepartmentMembers: IEntity
    {
        private DepartmentMembers()
        {
        }

        public int DepartmentId { get; private set; }
        public int MemberId { get; set; }
        public bool IsHeadOfDepartment { get; private set; }
        public DateTime DateJoined { get; private set; }
        public DateTime DateLeft { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }

        public Department Department { get; private set; }
        public Member Member { get; private set; }
    }
}
