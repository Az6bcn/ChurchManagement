namespace WebApi.Helpers;
#pragma warning disable CS1591
public class ApiRequestResponse<T> where T : class
#pragma warning restore CS1591
{
    public T Result { get; private set; }
    public bool Success { get; private set; }
    public ICollection<T> Results { get; private set; }
    public ICollection<string> ErrorMessage { get; private set; }


    /// <summary>
    /// Parse to my custom Response class
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static ApiRequestResponse<T> Succeed(T result)
        => new()
        {
            Success = true,
            Result = result
        };

    /// <summary>
    /// Parse to my custom Response class
    /// </summary>
    /// <param name="results"></param>
    /// <returns></returns>
    public static ApiRequestResponse<T> Succeed(ICollection<T> results)
        => new()
        {
            Success = true,
            Results = results
        };

    /// <summary>
    /// Parse to my custom Response class
    /// </summary>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public static ApiRequestResponse<T> Fail(params string[] errorMessage)
        => new()
        {
            ErrorMessage = errorMessage
        };

    /// <summary>
    /// Parse to my custom Response class
    /// </summary>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public static ApiRequestResponse<T> Fail(ICollection<string> errorMessage)
        => new()
        {
            ErrorMessage = errorMessage
        };
}