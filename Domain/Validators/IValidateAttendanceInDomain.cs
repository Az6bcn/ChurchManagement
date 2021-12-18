namespace Domain.Validators;

public interface IValidateAttendanceInDomain
{
    bool Validate(DateOnly serviceDate,
                  int male, 
                  int female,
                  int children,
                  int newComer,
                  out Dictionary<string, object> errors);
}