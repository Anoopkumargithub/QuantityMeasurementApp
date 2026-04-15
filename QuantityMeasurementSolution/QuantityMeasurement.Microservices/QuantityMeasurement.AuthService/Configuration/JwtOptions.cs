namespace QuantityMeasurement.AuthService.Configuration;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = "qma-auth-service";
    public string Audience { get; set; } = "qma-clients";
    public string SigningKey { get; set; } = "ChangeThisToAStrongSecretKeyForQMA2026!";
    public int ExpiryMinutes { get; set; } = 60;
}
