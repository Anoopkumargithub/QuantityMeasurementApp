using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurement.AuthService.Contracts;
using QuantityMeasurement.AuthService.Services;

namespace QuantityMeasurement.AuthService.Controllers;

[ApiController]
[Route("auth")]
public sealed class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthController(ITokenService tokenService, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _configuration = configuration;
    }

    [HttpPost("token")]
    [AllowAnonymous]
    public ActionResult<TokenResponse> CreateToken([FromBody] TokenRequest request)
    {
        var allowedUsername = _configuration["Auth:Username"] ?? "qma-admin";
        var allowedPassword = _configuration["Auth:Password"] ?? "qma-pass";

        if (!string.Equals(request.Username, allowedUsername, StringComparison.Ordinal) ||
            !string.Equals(request.Password, allowedPassword, StringComparison.Ordinal))
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        return Ok(_tokenService.CreateToken(request.Username));
    }

    [HttpGet("health")]
    [AllowAnonymous]
    public ActionResult<object> Health() => Ok(new { status = "OK" });

    [HttpGet("validate")]
    [Authorize]
    public ActionResult<object> Validate() => Ok(new { isValid = true, user = User.Identity?.Name });
}
