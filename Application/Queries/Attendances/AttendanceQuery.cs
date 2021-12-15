using Application.Interfaces.Repositories;
using Domain.Entities.AttendanceAggregate;

namespace Application.Queries.Attendances;

public class AttendanceQuery: IQueryAttendance
{
    private readonly IAttendanceRepositoryAsync _attendanceRepo;

    public AttendanceQuery(IAttendanceRepositoryAsync attendanceRepo)
    {
        _attendanceRepo = attendanceRepo;
    }
        
    public async Task<Attendance?> GetAttendanceByIdAndTenantIdAsync(int financeId, int tenantId)
        => await _attendanceRepo.GetAttendanceByIdAndTenantIdAsync(financeId, tenantId);
}