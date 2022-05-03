namespace Application.Contracts;

public interface ICurrentUser
{
    string? UserId { get; }
    bool IsAuthenticated { get; }
}