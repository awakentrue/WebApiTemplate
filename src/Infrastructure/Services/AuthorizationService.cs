using Application.Contracts.Authorization;
using Application.Dto;
using Application.Dto.Authorization;
using Application.Wrappers;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenFactory _tokenFactory;
    private readonly IRefreshTokenValidator _refreshTokenValidator;

    public AuthorizationService(IAuthenticationService authenticationService, ITokenFactory tokenFactory, IRefreshTokenValidator refreshTokenValidator)
    {
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        _tokenFactory = tokenFactory ?? throw new ArgumentNullException(nameof(tokenFactory));
        _refreshTokenValidator = refreshTokenValidator ?? throw new ArgumentNullException(nameof(refreshTokenValidator));
    }
    
    public async Task<Response<AuthorizationData>> AuthorizeAsync(string userName, string password)
    {
        var authenticationData = await _authenticationService.AuthenticateAsync(userName, password);
        
        var authorizationData = GetAuthorizationData(authenticationData);

        return Response.Success(authorizationData);
    }

    public async Task<Response<AuthorizationData>> RefreshTokenAsync(string refreshToken)
    {
        var authenticationData = await _refreshTokenValidator.ValidateAsync(refreshToken);

        var authorizationData = GetAuthorizationData(authenticationData);
        
        return Response.Success(authorizationData);
    }

    private AuthorizationData GetAuthorizationData(AuthenticationData authenticationData)
    {
        var accessToken = _tokenFactory.CreateAccessToken(authenticationData.UserId);
        var refreshToken = _tokenFactory.CreateRefreshToken(authenticationData.UserId);

        return new AuthorizationData(accessToken, refreshToken);
    }
}