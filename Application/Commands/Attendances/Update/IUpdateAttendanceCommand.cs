using Application.Dtos.Request.Update;

namespace Application.Commands.Attendances.Update;

public interface IUpdateAttendanceCommand
{
    Task ExecuteAsync(UpdateAttendanceRequestDto request);
}