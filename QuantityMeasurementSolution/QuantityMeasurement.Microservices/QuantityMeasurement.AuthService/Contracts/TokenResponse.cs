namespace QuantityMeasurement.AuthService.Contracts;

public sealed class TokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
}
