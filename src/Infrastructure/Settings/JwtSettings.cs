namespace Infrastructure.Settings;

public class JwtSettings
{
    public string AccessTokenSecret { get; set; } = null!;

    public double AccessTokenExpirationMinutes { get; set; }

    public string RefreshTokenSecret { get; set; } = null!;

    public double RefreshTokenExpirationMinutes { get; set; }

    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;
}