namespace Application.Wrappers;

public class ResponseError
{
    public ResponseError(string message, string? code = null, string? documentationLink = null)
    {
        Message = message;
        Code = code;
        DocumentationLink = documentationLink;
    }
    
    public string? Code { get; }
    
    public string? Message { get; }
    
    public string? DocumentationLink { get; }

    public static ResponseError New(string message, string? code = null, string? documentationLink = null)
    {
        return new ResponseError(message, code, documentationLink);
    }
}