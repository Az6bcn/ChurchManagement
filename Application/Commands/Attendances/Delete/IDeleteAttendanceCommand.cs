namespace Application.Commands.Attendances.Delete;

public interface IDeleteAttendanceCommand
{
    Task ExecuteAsync(int attendanceId, int tenantId);
}