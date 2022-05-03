using Application.Contracts.Authorization;
using Application.Dto.Authorization;
using Application.Dto.Requests;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AuthorizationController : ApiControllerBase
{
    private readonly IAuthorizationService _authorizationService;

    public AuthorizationController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
    }

    [HttpPost("login")]
    public async Task<Response<AuthorizationData>> Login([FromBody] LoginRequest loginRequest)
    {
        return await _authorizationService.AuthorizeAsync(loginRequest.UserName, loginRequest.Password);
    }

    [HttpPost("refresh")]
    public async Task<Response<AuthorizationData>> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
    {
        return await _authorizationService.RefreshTokenAsync(refreshTokenRequest.RefreshToken);
    }
}