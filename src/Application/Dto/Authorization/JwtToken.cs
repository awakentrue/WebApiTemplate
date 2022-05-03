namespace Application.Dto.Authorization;

public class JwtToken
{
    public JwtToken(string token, DateTime expiresIn)
    {
        Token = token;
        ExpiresIn = expiresIn;
    }

    public string Token { get; }
    
    public DateTime ExpiresIn { get; }
}