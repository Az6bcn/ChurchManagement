using System.Threading.Tasks;
using Domain.Entities.AttendanceAggregate;

namespace Application.Interfaces.Repositories
{
    public interface IAttendanceRepositoryAsync: IGenericRepositoryAsync<Attendance>
    {
        Task<Attendance?> GetAttendanceByIdAndTenantIdAsync(int attendanceId,
                                                            int tenantId);
    }
}