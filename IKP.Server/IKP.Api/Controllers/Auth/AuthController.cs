using System.Security.Claims;
using IKP.Application.Auth.Interfaces;
using IKP.Application.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace IKP.Api.Controllers.Auth;

/// <summary>
/// Provides the initial account bootstrap and authentication endpoints.
/// </summary>
[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IUserAuthenticationService authenticationService;

    /// <summary>
    /// Initializes the authentication controller.
    /// </summary>
    public AuthController(IUserAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    /// <summary>
    /// Creates the first account. The endpoint becomes permanently unavailable after the first account exists.
    /// </summary>
    [EnableRateLimiting("auth")]
    [HttpPost("bootstrap")]
    [ProducesResponseType<AuthTokenResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Bootstrap([FromBody] AuthBootstrapRequest request, CancellationToken cancellationToken)
    {
        Response.Headers["Cache-Control"] = "no-store";
        Response.Headers["Pragma"] = "no-cache";
        AuthTokenResponse response = await authenticationService.BootstrapAsync(request, GetIpAddress(), Request.Headers["User-Agent"].ToString(), cancellationToken);
        
        return StatusCode(StatusCodes.Status201Created, response);
    }

    /// <summary>
    /// Authenticates with a username or email and password.
    /// </summary>
    [EnableRateLimiting("auth")]
    [HttpPost("login")]
    [ProducesResponseType<AuthTokenResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthTokenResponse>> Login([FromBody] AuthLoginRequest request, CancellationToken cancellationToken)
    {
        Response.Headers["Cache-Control"] = "no-store";
        Response.Headers["Pragma"] = "no-cache";
        AuthTokenResponse response = await authenticationService.LoginAsync(request, GetIpAddress(), Request.Headers["User-Agent"].ToString(), cancellationToken);
        
        return Ok(response);
    }

    /// <summary>
    /// Rotates a refresh token and issues a new access token.
    /// </summary>
    [EnableRateLimiting("auth")]
    [HttpPost("refresh")]
    [ProducesResponseType<AuthTokenResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthTokenResponse>> Refresh([FromBody] AuthRefreshRequest request, CancellationToken cancellationToken)
    {
        Response.Headers["Cache-Control"] = "no-store";
        Response.Headers["Pragma"] = "no-cache";
        AuthTokenResponse response = await authenticationService.RefreshAsync(request.RefreshToken, GetIpAddress(), Request.Headers["User-Agent"].ToString(), cancellationToken);
        
        return Ok(response);
    }

    /// <summary>
    /// Revokes the session represented by a refresh token.
    /// </summary>
    [EnableRateLimiting("auth")]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Logout([FromBody] AuthRefreshRequest request, CancellationToken cancellationToken)
    {
        await authenticationService.LogoutAsync(request.RefreshToken, cancellationToken);
        
        return NoContent();
    }

    /// <summary>
    /// Returns the authenticated user's public profile.
    /// </summary>
    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType<AuthUserSummary>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthUserSummary>> Me(CancellationToken cancellationToken)
    {
        Guid? userId = GetUserId();

        if (!userId.HasValue) return Unauthorized();
        
        AuthUserSummary? user = await authenticationService.GetUserAsync(userId.Value, cancellationToken);
        
        return user is null ? Unauthorized() : Ok(user);
    }

    /// <summary>
    /// Changes the authenticated user's password and revokes all refresh-token sessions.
    /// </summary>
    [Authorize]
    [HttpPost("password/change")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword([FromBody] AuthChangePasswordRequest request, CancellationToken cancellationToken)
    {
        Guid? userId = GetUserId();

        if (!userId.HasValue) return Unauthorized();

        await authenticationService.ChangePasswordAsync(userId.Value, request, cancellationToken);
        
        return NoContent();
    }

    private Guid? GetUserId()
    {
        string? value = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        return Guid.TryParse(value, out Guid userId) ? userId : null;
    }

    private string? GetIpAddress() => HttpContext.Connection.RemoteIpAddress?.ToString();
}
