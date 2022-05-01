using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Contracts.Authorization;
using Application.Dto.Authorization;
using Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtTokenFactory : ITokenFactory
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenFactory(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
    }

    public JwtToken CreateAccessToken(string userId) 
        => GenerateToken(_jwtSettings.AccessTokenSecret, _jwtSettings.AccessTokenExpirationMinutes, userId);

    public JwtToken CreateRefreshToken(string userId) 
        => GenerateToken(_jwtSettings.RefreshTokenSecret, _jwtSettings.RefreshTokenExpirationMinutes, userId);

    private JwtToken GenerateToken(string secretKey, double expirationMinutes, string userId)
    {
        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);
        var notBefore = DateTime.UtcNow;
        var expires = DateTime.UtcNow.AddMinutes(expirationMinutes);
        var audience = _jwtSettings.Audience;
        var issuer = _jwtSettings.Issuer;
        var token = new JwtSecurityToken(
            audience:audience,
            issuer: issuer,
            notBefore: notBefore,
            expires: expires,
            claims: claims,
            signingCredentials: credentials);

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new JwtToken(serializedToken, token.ValidTo);
    }
}