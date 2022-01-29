namespace Application.Exceptions;

public sealed class ValidationException : Exception
{
    public ValidationException() : base() { }

    public ValidationException(string message) : base(message) { }

    public ValidationException(string message, IDictionary<string, object> validationErrors) : base(message)
    {
        foreach (var err in validationErrors)
        {
            Data.Add(err.Key, err.Value);
        }
    }

    public ValidationException(string message, Exception ex) : base(message, ex) { }
}