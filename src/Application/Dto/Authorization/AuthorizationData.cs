namespace Application.Dto.Authorization;

public class AuthorizationData
{
    public AuthorizationData(JwtToken accessToken, JwtToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public JwtToken AccessToken { get; }

    public JwtToken RefreshToken { get; }
}