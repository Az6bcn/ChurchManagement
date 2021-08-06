using System.Threading.Tasks;

namespace Application.Queries.Attendance
{
    public interface IQueryAttendance
    {
        Task<Domain.Entities.AttendanceAggregate.Attendance?> GetAttendanceByIdAndTenantIdAsync(int financeId, int tenantId);
    }
}