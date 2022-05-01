using Application.Dto.Authorization;

namespace Application.Contracts.Authorization;

public interface IRefreshTokenValidator
{
    Task<AuthenticationData> ValidateAsync(string refreshToken);
}