using Application.Dto.Authorization;

namespace Application.Contracts.Authorization;

public interface ITokenFactory
{
    JwtToken CreateAccessToken(string userId);
    JwtToken CreateRefreshToken(string userId);
}