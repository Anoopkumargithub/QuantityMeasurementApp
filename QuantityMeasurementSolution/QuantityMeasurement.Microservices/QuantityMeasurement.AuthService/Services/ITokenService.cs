using QuantityMeasurement.AuthService.Contracts;

namespace QuantityMeasurement.AuthService.Services;

public interface ITokenService
{
    TokenResponse CreateToken(string username);
}
