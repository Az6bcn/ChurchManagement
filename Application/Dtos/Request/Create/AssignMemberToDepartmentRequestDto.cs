using System;

namespace Application.Dtos.Request.Create
{
    public class AssignMemberToDepartmentRequestDto
    {
        public int MemberId { get; set; }
        public int DepartmentId { get; set; }
        public int TenantId { get; set; }
        public bool IsHeadOfDepartment { get; set; }
        public DateTime DateJoined { get; set; }
    }
}