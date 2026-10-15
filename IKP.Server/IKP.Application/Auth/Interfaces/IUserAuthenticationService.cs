using IKP.Application.Auth.Models;

namespace IKP.Application.Auth.Interfaces;

/// <summary>
/// Provides user bootstrap, sign-in, refresh, sign-out and password-change operations.
/// </summary>
public interface IUserAuthenticationService
{
    /// <summary>
    /// Creates the first account only when no account exists and the one-time secret matches.
    /// </summary>
    ValueTask<AuthTokenResponse> BootstrapAsync(AuthBootstrapRequest request, string? ipAddress, string? userAgent, CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates a username/email and password and issues a session.
    /// </summary>
    ValueTask<AuthTokenResponse> LoginAsync(AuthLoginRequest request, string? ipAddress, string? userAgent, CancellationToken cancellationToken = default);

    /// <summary>
    /// Rotates a refresh token and issues a new access token.
    /// </summary>
    ValueTask<AuthTokenResponse> RefreshAsync(string refreshToken, string? ipAddress, string? userAgent, CancellationToken cancellationToken = default);

    /// <summary>
    /// Revokes the session represented by a refresh token.
    /// </summary>
    ValueTask LogoutAsync(string refreshToken, CancellationToken cancellationToken = default);

    /// <summary>
    /// Changes a user's password and revokes all active sessions.
    /// </summary>
    ValueTask ChangePasswordAsync(Guid userId, AuthChangePasswordRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the public profile for a user.
    /// </summary>
    ValueTask<AuthUserSummary?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
