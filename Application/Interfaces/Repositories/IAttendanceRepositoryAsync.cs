using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.AttendanceAggregate;

namespace Application.Interfaces.Repositories
{
    public interface IAttendanceRepositoryAsync : IGenericRepositoryAsync<Attendance>
    {
        Task<Attendance?> GetAttendanceByIdAndTenantIdAsync(int attendanceId,
                                                            int tenantId);

        Task<IEnumerable<Attendance>> GetAttendancesBetweenDatesByTenantIdAsync(int tenantId,
                                                                                DateOnly startDate,
                                                                                DateOnly endDate);

        Task<IEnumerable<Attendance>> GetAttendancesForLastYearAndCurrentYearByTenantIdAsync(int tenantId,
                                                                                             DateOnly startDate,
                                                                                             DateOnly endDate);
    }
}