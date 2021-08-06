using System.Threading.Tasks;

namespace Application.Commands.Attendance.Delete
{
    public interface IDeleteAttendanceCommand
    {
        Task ExecuteAsync(int attendanceId, int tenantId);
    }
}
