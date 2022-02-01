namespace Shared.Communications;

public class NotificationOutput
{
    private Dictionary<string, object> _errors;

    public NotificationOutput()
    {
        _errors = new();
    }

    public bool HasErrors => _errors.Any();

    public IDictionary<string, Object> Errors => _errors;

    public void AddError(string key, string message) => _errors.Add(key, message);

    public void AddErrors(IDictionary<string, object> errors)
    {
        foreach ((string key, object value) in errors)
            _errors.Add(key, value);
    }
}