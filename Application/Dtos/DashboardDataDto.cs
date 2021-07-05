using System;
using System.Collections.Generic;

namespace Application.Dtos
{
    public class DashboardDataDto
    {
        public int Members{ get; set; }
        public decimal Tithe { get; set; }
        public decimal Thanksgiving { get; set; }
        public decimal Expenses { get; set; }
        public decimal Offering { get; set; }
        public int MonthAttendance { get; set; }
        public IDictionary<DateTime, AttendanceDto> Attendance { get; set; }
        public int NewComers { get; set; }
    }
}