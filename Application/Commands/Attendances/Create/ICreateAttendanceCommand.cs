using Application.Dtos.Request.Create;

namespace Application.Commands.Attendances.Create;

public interface ICreateAttendanceCommand
{
    Task ExecuteAsync(CreateAttendanceRequestDto request);
}