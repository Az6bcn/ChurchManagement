using System.Threading.Tasks;
using Application.Dtos.Request.Update;

namespace Application.Commands.Attendance.Update
{
    public interface IUpdateAttendanceCommand
    {
        Task ExecuteAsync(UpdateAttendanceRequestDto request);
    }
}