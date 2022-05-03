namespace WebApi.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IEnumerable<string> errors)
    {
        Errors = errors;
    }
    
    public IEnumerable<string> Errors { get; }
}