namespace Application.Dto.Authorization;

public class AuthenticationData
{
    public AuthenticationData(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; }
}