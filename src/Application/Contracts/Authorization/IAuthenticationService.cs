using Application.Dto.Authorization;

namespace Application.Contracts.Authorization;

public interface IAuthenticationService
{
    Task<AuthenticationData> AuthenticateAsync(string userName, string password);
}