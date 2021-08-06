using System.Threading.Tasks;
using Application.Dtos.Request.Create;

namespace Application.Commands.Attendance.Create
{
    public interface ICreateAttendanceCommand
    {
        Task ExecuteAsync(CreateAttendanceRequestDto request);
    }
}