using System;
using Domain.Abstracts;

namespace Domain.Entities
{
    public class DepartmentMembers: Entity
    {
        private DepartmentMembers()
        {
        }

        public int DepartmentId { get; private set; }
        public int MemberId { get; set; }
        public bool IsHeadOfDepartment { get; private set; }
        public DateTime DateJoined { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Department Department { get; private set; }
        public Member Member { get; private set; }
    }
}
