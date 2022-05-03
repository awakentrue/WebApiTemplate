namespace Application.Wrappers;

public static class ResponseErrors
{
    public static class Common
    {
        public static ResponseError InternalServerError(Guid errorGuid)
        {
            return ResponseError.New("Internal Server Error", errorGuid.ToString());
        }
        
        public static readonly ResponseError Unauthorized = ResponseError.New("Unauthorized");
    }
}