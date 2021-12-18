using Domain.Entities.AttendanceAggregate;

namespace Application.Queries.Attendances;

public interface IQueryAttendance
{
    Task<Attendance?> GetAttendanceByIdAndTenantIdAsync(int financeId, int tenantId);
}