using System.Threading.Tasks;
using Application.Interfaces.Repositories;

namespace Application.Queries.Attendance
{
    public class AttendanceQuery: IQueryAttendance
    {
        private readonly IAttendanceRepositoryAsync _attendanceRepo;

        public AttendanceQuery(IAttendanceRepositoryAsync attendanceRepo)
        {
            _attendanceRepo = attendanceRepo;
        }
        
        public async Task<Domain.Entities.AttendanceAggregate.Attendance?> GetAttendanceByIdAndTenantIdAsync(int financeId, int tenantId)
            => await _attendanceRepo.GetAttendanceByIdAndTenantIdAsync(financeId, tenantId);
    }
}