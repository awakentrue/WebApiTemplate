using Application.Dto.Authorization;
using Application.Wrappers;

namespace Application.Contracts.Authorization;

public interface IAuthorizationService
{
    Task<Response<AuthorizationData>> AuthorizeAsync(string userName, string password);
    Task<Response<AuthorizationData>> RefreshTokenAsync(string refreshToken);
}