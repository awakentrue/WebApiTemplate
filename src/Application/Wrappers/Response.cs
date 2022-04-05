namespace Application.Wrappers;

public class Response<T> : Response
{
    public Response(T data)
    {
        Data = data;
    }

    public Response(ResponseError[] errors) : base(errors)
    {
    }

    public T? Data { get; }

    public static implicit operator Response<T>(T data) => new Response<T>(data);

    public static implicit operator Response<T>(ResponseError[] errors) => new Response<T>(errors);
}

public class Response
{
    protected Response()
    {
        IsSuccessful = true;
    }

    protected Response(ResponseError[] errors)
    {
        IsSuccessful = false;
        Errors = errors;
    }
    
    public bool IsSuccessful { get; }
    
    public ResponseError[]? Errors { get; }
    
    public static Response<T> Success<T>(T data)
    {
        return new Response<T>(data);
    }

    public static Response<T> Fail<T>(params ResponseError[] errors)
    {
        return new Response<T>(errors);
    }

    public static Response<T> Fail<T>(IEnumerable<ResponseError> errors)
    {
        return new Response<T>(errors.ToArray());
    }
}