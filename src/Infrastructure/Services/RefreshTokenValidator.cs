using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.Contracts.Authorization;
using Application.Dto.Authorization;
using Application.Exceptions;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class RefreshTokenValidator : IRefreshTokenValidator
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<IdentityUser> _userManager;

    public RefreshTokenValidator(JwtSettings jwtSettings, UserManager<IdentityUser> userManager)
    {
        _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<AuthenticationData> ValidateAsync(string refreshToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.RefreshTokenSecret)),
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out _);
            var user = await _userManager.GetUserAsync(principal);

            return new AuthenticationData(user.Id);
        }
        catch (Exception)
        {
            throw new ApiException("Invalid Refresh Token");
        }
    }
}