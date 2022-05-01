namespace Application.Dto.Authorization;

public class JwtToken
{
    public JwtToken(string token, DateTime expiredIn)
    {
        Token = token;
        ExpiredIn = expiredIn;
    }

    public string Token { get; }
    
    public DateTime ExpiredIn { get; }
}