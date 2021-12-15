namespace Domain.Validators;

public sealed class DomainValidationException : Exception
{
    public DomainValidationException() : base()
    {
    }

    public DomainValidationException(string message) : base(message)
    {
    }

    public DomainValidationException(string message,
                                     IDictionary<string, object> validationErrors) : base(message)
    {
        foreach (var err in validationErrors)
        {
            Data.Add(err.Key, err.Value);
        }
    }

    public DomainValidationException(string message,
                                     Exception ex) : base(message, ex)
    {
    }
}