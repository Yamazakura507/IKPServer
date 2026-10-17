using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IKP.Application.Auth.Exceptions;
using IKP.Application.Auth.Interfaces;
using IKP.Application.Auth.Models;
using IKP.Domain.Common.Auth;
using IKP.Domain.Entities.Auth.Credentials;
using IKP.Domain.Entities.Auth.Lookup;
using IKP.Domain.Entities.Auth.Sessions;
using IKP.Domain.Entities.Auth.Roles;
using IKP.Domain.Entities.Auth.Users;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IKP.Infrastructure.Services.Auth;

/// <summary>
/// Implements password authentication and rotating refresh-token sessions.
/// </summary>
public sealed class UserAuthenticationService : IUserAuthenticationService
{
    private readonly AppDbContext db;
    private readonly IPasswordHasher<AuthUser> passwordHasher;
    private readonly AuthOptions options;
    private readonly byte[] signingKey;

    /// <summary>
    /// Initializes the authentication service.
    /// </summary>
    public UserAuthenticationService(AppDbContext db, IPasswordHasher<AuthUser> passwordHasher, IOptions<AuthOptions> options)
    {
        this.db = db;
        this.passwordHasher = passwordHasher;
        this.options = options.Value;
        this.signingKey = Encoding.UTF8.GetBytes(this.options.SigningKey ?? string.Empty);
    }

    /// <inheritdoc />
    public async ValueTask<AuthTokenResponse> BootstrapAsync(AuthBootstrapRequest request, string? ipAddress, string? userAgent, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidateAccountInput(request.UserName, request.DisplayName, request.Email, request.Password);

        string? configuredSecret = options.BootstrapSecret;

        if (string.IsNullOrWhiteSpace(configuredSecret)) throw new AuthOperationException(404, "BOOTSTRAP_DISABLED", "Initial account bootstrap is not enabled.");

        byte[] expectedSecret = Encoding.UTF8.GetBytes(configuredSecret);
        byte[] suppliedSecret = Encoding.UTF8.GetBytes(request.BootstrapSecret ?? string.Empty);

        if (expectedSecret.Length != suppliedSecret.Length || !CryptographicOperations.FixedTimeEquals(expectedSecret, suppliedSecret))
            throw new AuthOperationException(403, "BOOTSTRAP_FORBIDDEN", "The bootstrap secret is invalid.");

        await using IDbContextTransaction transaction = await db.Database.BeginTransactionAsync(cancellationToken);
        string bootstrapLockKey = "ikp-auth-bootstrap";

        await db.Database.ExecuteSqlInterpolatedAsync($"SELECT pg_advisory_xact_lock(hashtextextended({bootstrapLockKey}, 0));", cancellationToken);

        if (await db.AuthUsers.IgnoreQueryFilters().AnyAsync(cancellationToken)) throw new AuthOperationException(409, "BOOTSTRAP_ALREADY_COMPLETED", "An account already exists. Bootstrap is permanently disabled.");

        string normalizedUserName = Normalize(request.UserName);
        string? normalizedEmail = NormalizeNullable(request.Email);
        AuthUser user = new()
        {
            UserName = request.UserName.Trim(),
            NormalizedUserName = normalizedUserName,
            Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim(),
            NormalizedEmail = normalizedEmail,
            DisplayName = request.DisplayName.Trim(),
            IsBootstrapAccount = true,
            IsActive = true
        };
        string passwordHash = passwordHasher.HashPassword(user, request.Password);
        AuthenticationMethodDefinition method = await GetPasswordMethodAsync(cancellationToken);
        UserCredential credential = new()
        {
            User = user,
            AuthenticationMethodId = method.Id,
            SecretHash = passwordHash,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        db.AuthUsers.Add(user);
        db.UserCredentials.Add(credential);

        AuthRole? systemAdministratorRole = await db.AuthRoles.FirstOrDefaultAsync(x => x.Code == AuthorizationRoleCodes.SystemAdministrator && x.IsActive, cancellationToken);
        
        if (systemAdministratorRole is null) throw new InvalidOperationException("The system administrator role has not been seeded. Apply the latest EF migration.");
        
        user.UserRoles.Add(new AuthUserRole
        {
            UserId = user.Id,
            RoleId = systemAdministratorRole.Id,
            CreatedAt = DateTime.UtcNow
        });

        await db.SaveChangesAsync(cancellationToken);

        AuthTokenResponse response = await CreateSessionAsync(user, ipAddress, userAgent, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return response;
    }

    /// <inheritdoc />
    public async ValueTask<AuthTokenResponse> LoginAsync(AuthLoginRequest request, string? ipAddress, string? userAgent, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.UserNameOrEmail) || string.IsNullOrEmpty(request.Password)) throw InvalidCredentials();

        string normalizedIdentifier = Normalize(request.UserNameOrEmail);

        Guid? candidateUserId = await db.AuthUsers.AsNoTracking()
            .Where(x => x.NormalizedUserName == normalizedIdentifier || x.NormalizedEmail == normalizedIdentifier)
            .OrderByDescending(x => x.NormalizedUserName == normalizedIdentifier)
            .Select(x => (Guid?)x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (!candidateUserId.HasValue) throw InvalidCredentials();

        await using IDbContextTransaction transaction = await db.Database.BeginTransactionAsync(cancellationToken);
        await db.Database.ExecuteSqlInterpolatedAsync($"SELECT pg_advisory_xact_lock(hashtextextended(CAST({candidateUserId.Value} AS text), 4));", cancellationToken);
        
        AuthUser? user = await db.AuthUsers
            .Include(x => x.Credentials)
            .ThenInclude(x => x.AuthenticationMethod)
            .FirstOrDefaultAsync(x => x.Id == candidateUserId.Value, cancellationToken);

        DateTime now = DateTime.UtcNow;

        if (user is null || user.IsDeleted || !user.IsActive || (user.LockoutEndAt.HasValue && user.LockoutEndAt.Value > now)) throw InvalidCredentials();

        UserCredential? credential = user.Credentials.FirstOrDefault(x => x.IsActive && x.AuthenticationMethod.Code == AuthenticationMethodCodes.Password && x.AuthenticationMethod.IsActive);
        
        if (credential is null) throw InvalidCredentials();

        PasswordVerificationResult verification = passwordHasher.VerifyHashedPassword(user, credential.SecretHash, request.Password);

        if (verification == PasswordVerificationResult.Failed)
        {
            user.FailedLoginCount++;

            if (user.FailedLoginCount >= options.MaximumFailedLoginAttempts)
            {
                user.LockoutEndAt = now.AddMinutes(options.LockoutMinutes);
                user.FailedLoginCount = 0;
            }

            await db.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            throw InvalidCredentials();
        }

        if (verification == PasswordVerificationResult.SuccessRehashNeeded) credential.SecretHash = passwordHasher.HashPassword(user, request.Password);

        user.FailedLoginCount = 0;
        user.LockoutEndAt = null;
        user.LastLoginAt = now;
        credential.LastUsedAt = now;

        await db.SaveChangesAsync(cancellationToken);

        AuthTokenResponse response = await CreateSessionAsync(user, ipAddress, userAgent, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return response;
    }

    /// <inheritdoc />
    public async ValueTask<AuthTokenResponse> RefreshAsync(string refreshToken, string? ipAddress, string? userAgent, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(refreshToken)) throw InvalidRefreshToken();

        string tokenHash = HashRefreshToken(refreshToken);

        await using IDbContextTransaction transaction = await db.Database.BeginTransactionAsync(cancellationToken);
        Guid? initialSessionId = await db.UserSessions.AsNoTracking().Where(x => x.RefreshTokenHash == tokenHash).Select(x => (Guid?)x.Id).FirstOrDefaultAsync(cancellationToken);
        
        if (!initialSessionId.HasValue) throw InvalidRefreshToken();

        await db.Database.ExecuteSqlInterpolatedAsync($"SELECT pg_advisory_xact_lock(hashtextextended(CAST({initialSessionId.Value} AS text), 3));", cancellationToken);
        
        UserSession? session = await db.UserSessions.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == initialSessionId.Value, cancellationToken);
        
        if (session is null) throw InvalidRefreshToken();

        DateTime now = DateTime.UtcNow;

        if (session.RevokedAt.HasValue)
        {
            await RevokeAllSessionsAsync(session.UserId, now, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            throw InvalidRefreshToken();
        }

        if (session.ExpiresAt <= now || !session.User.IsActive || session.User.IsDeleted)
        {
            session.RevokedAt = now;
            await db.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            throw InvalidRefreshToken();
        }

        AuthUser user = session.User;
        AuthTokenResponse response = await CreateSessionAsync(user, ipAddress, userAgent, cancellationToken);
        string replacementHash = HashRefreshToken(response.RefreshToken);
        UserSession replacement = await db.UserSessions.FirstAsync(x => x.RefreshTokenHash == replacementHash, cancellationToken);
        session.RevokedAt = now;
        session.LastUsedAt = now;
        session.ReplacedBySessionId = replacement.Id;

        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return response;
    }

    /// <inheritdoc />
    public async ValueTask LogoutAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(refreshToken)) return;

        string tokenHash = HashRefreshToken(refreshToken);
        Guid? sessionId = await db.UserSessions.AsNoTracking().Where(x => x.RefreshTokenHash == tokenHash).Select(x => (Guid?)x.Id).FirstOrDefaultAsync(cancellationToken);
        
        if (!sessionId.HasValue) return;

        await using IDbContextTransaction transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        await db.Database.ExecuteSqlInterpolatedAsync($"SELECT pg_advisory_xact_lock(hashtextextended(CAST({sessionId.Value} AS text), 3));", cancellationToken);
        
        UserSession? session = await db.UserSessions.FirstOrDefaultAsync(x => x.Id == sessionId.Value, cancellationToken);
        
        if (session is not null && !session.RevokedAt.HasValue)
        {
            session.RevokedAt = DateTime.UtcNow;

            await db.SaveChangesAsync(cancellationToken);
        }

        await transaction.CommitAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask ChangePasswordAsync(Guid userId, AuthChangePasswordRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidatePassword(request.NewPassword);

        AuthUser? user = await db.AuthUsers.Include(x => x.Credentials).ThenInclude(x => x.AuthenticationMethod).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        
        if (user is null || user.IsDeleted || !user.IsActive) throw InvalidCredentials();

        UserCredential? credential = user.Credentials.FirstOrDefault(x => x.IsActive && x.AuthenticationMethod.Code == AuthenticationMethodCodes.Password);
       
        if (credential is null || passwordHasher.VerifyHashedPassword(user, credential.SecretHash, request.CurrentPassword) == PasswordVerificationResult.Failed)
            throw new AuthOperationException(400, "CURRENT_PASSWORD_INVALID", "The current password is incorrect.");

        if (string.Equals(request.CurrentPassword, request.NewPassword, StringComparison.Ordinal))
            throw new AuthOperationException(400, "PASSWORD_UNCHANGED", "The new password must differ from the current password.");

        DateTime now = DateTime.UtcNow;
        credential.SecretHash = passwordHasher.HashPassword(user, request.NewPassword);
        credential.UpdatedAt = now;
        credential.MustChangePassword = false;
        List<UserSession> activeSessions = await db.UserSessions.Where(x => x.UserId == userId && x.RevokedAt == null).ToListAsync(cancellationToken);

        foreach (UserSession session in activeSessions)
        {
            session.RevokedAt = now;
        }
        
        await db.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<AuthUserSummary?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        AuthUser? user = await db.AuthUsers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId && x.IsActive && !x.IsDeleted, cancellationToken);
        
        return user is null ? null : ToSummary(user);
    }

    private async ValueTask<AuthTokenResponse> CreateSessionAsync(AuthUser user, string? ipAddress, string? userAgent, CancellationToken cancellationToken)
    {
        DateTime now = DateTime.UtcNow;
        string refreshToken = CreateRefreshToken();

        UserSession session = new()
        {
            UserId = user.Id,
            RefreshTokenHash = HashRefreshToken(refreshToken),
            CreatedAt = now,
            ExpiresAt = now.AddDays(options.RefreshTokenDays),
            IpAddress = Limit(ipAddress, 64),
            UserAgent = Limit(userAgent, 512)
        };

        db.UserSessions.Add(session);
        await db.SaveChangesAsync(cancellationToken);

        if (user.IsBootstrapAccount && !await db.AuthUserRoles.AnyAsync(x => x.UserId == user.Id && x.Role.Code == AuthorizationRoleCodes.SystemAdministrator, cancellationToken))
        {
            AuthRole? systemAdministratorRole = await db.AuthRoles.FirstOrDefaultAsync(x => x.Code == AuthorizationRoleCodes.SystemAdministrator && x.IsActive, cancellationToken);
            
            if (systemAdministratorRole is null) throw new InvalidOperationException("The system administrator role has not been seeded. Apply the latest EF migration.");
            
            db.AuthUserRoles.Add(new AuthUserRole
            {
                UserId = user.Id,
                RoleId = systemAdministratorRole.Id,
                CreatedAt = DateTime.UtcNow
            });

            await db.SaveChangesAsync(cancellationToken);
        }

        int expiresIn = checked(options.AccessTokenMinutes * 60);
        DateTime expiresAt = now.AddMinutes(options.AccessTokenMinutes);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new("sid", session.Id.ToString()),
            new("display_name", user.DisplayName)
        ];

        List<string> roleCodes = await db.AuthUserRoles
            .Where(x => x.UserId == user.Id && x.Role.IsActive)
            .Select(x => x.Role.Code)
            .Distinct()
            .ToListAsync(cancellationToken);

        List<string> permissionCodes = await db.AuthUserRoles
            .Where(x => x.UserId == user.Id && x.Role.IsActive)
            .SelectMany(x => x.Role.RolePermissions)
            .Where(x => x.Permission.IsActive)
            .Select(x => x.Permission.Code)
            .Distinct()
            .ToListAsync(cancellationToken);

        claims.AddRange(roleCodes.Select(x => new Claim(ClaimTypes.Role, x)));
        claims.AddRange(permissionCodes.Select(x => new Claim("permission", x)));

        SigningCredentials credentials = new(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new(options.Issuer, options.Audience, claims, now, expiresAt, credentials);

        return new AuthTokenResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken,
            ExpiresIn = expiresIn,
            User = ToSummary(user)
        };
    }

    private async ValueTask<AuthenticationMethodDefinition> GetPasswordMethodAsync(CancellationToken cancellationToken)
    {
        AuthenticationMethodDefinition? method = await db.AuthenticationMethods.FirstOrDefaultAsync(x => x.Code == AuthenticationMethodCodes.Password && x.IsActive, cancellationToken);
        
        return method ?? throw new InvalidOperationException("The PASSWORD authentication method has not been seeded. Apply the latest EF migration.");
    }

    private async ValueTask RevokeAllSessionsAsync(Guid userId, DateTime now, CancellationToken cancellationToken)
    {
        List<UserSession> sessions = await db.UserSessions.Where(x => x.UserId == userId && x.RevokedAt == null).ToListAsync(cancellationToken);

        foreach (UserSession session in sessions)
        {
            session.RevokedAt = now;
        }
    }

    private void ValidateAccountInput(string userName, string displayName, string? email, string password)
    {
        if (string.IsNullOrWhiteSpace(userName) || userName.Trim().Length > 100)
            throw new AuthOperationException(400, "USERNAME_INVALID", "The username is required and must not exceed 100 characters.");
        if (string.IsNullOrWhiteSpace(displayName) || displayName.Trim().Length > 200)
            throw new AuthOperationException(400, "DISPLAY_NAME_INVALID", "The display name is required and must not exceed 200 characters.");
        if (!string.IsNullOrWhiteSpace(email) && (email.Length > 320 || !email.Contains('@')))
            throw new AuthOperationException(400, "EMAIL_INVALID", "The email address is invalid.");

        ValidatePassword(password);
    }

    private void ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < options.MinimumPasswordLength || password.Length > 1024)
            throw new AuthOperationException(400, "PASSWORD_POLICY_FAILED", $"The password must contain between {options.MinimumPasswordLength} and 1024 characters.");
    }

    private static AuthOperationException InvalidCredentials() => new(401, "INVALID_CREDENTIALS", "The supplied credentials are invalid or the account is unavailable.");

    private static AuthOperationException InvalidRefreshToken() => new(401, "INVALID_REFRESH_TOKEN", "The refresh token is invalid, expired, or revoked.");

    private static AuthUserSummary ToSummary(AuthUser user) => new()
    {
        Id = user.Id,
        UserName = user.UserName,
        Email = user.Email,
        DisplayName = user.DisplayName
    };

    private static string Normalize(string value) => value.Trim().ToUpperInvariant();

    private static string? NormalizeNullable(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim().ToUpperInvariant();

    private static string CreateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)).TrimEnd('=').Replace('+', '-').Replace('/', '_');

    private static string HashRefreshToken(string token) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));

    private static string? Limit(string? value, int maximumLength) => string.IsNullOrWhiteSpace(value) ? null : value.Length <= maximumLength ? value : value[..maximumLength];
}
