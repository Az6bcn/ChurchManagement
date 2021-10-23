using System.Collections.Generic;
using System.Linq;
using Domain.Entities.AttendanceAggregate;

namespace Application.Dtos.Response.Get
{
    public class GetAttendanceResponseDto
    {
        public int Men { get; set; }
        public int Women { get; set; }
        public int Children { get; set; }
        public int Total => Men + Women + Children;

        public GetAttendanceResponseDto(ICollection<Attendance> attendance)
        {
            Men = attendance.Sum(x => x.Male);
            Women = attendance.Sum(x => x.Female);
            Children = attendance.Sum(x => x.Children);
        }
    }
}