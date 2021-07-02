using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.AttendanceAggregate;

namespace Application.Dtos
{
    public class AttendanceDto
    {
        public int Men { get; set; }
        public int Women { get; set; }
        public int Children { get; set; }
        public int Total => Men + Women + Children;

        public AttendanceDto(IEnumerable<Attendance> attendance)
        {
            Men = attendance.Sum(x => x.Male);
            Women = attendance.Sum(x => x.Female);
            Children = attendance.Sum(x => x.Children);
            
        }
    }
}