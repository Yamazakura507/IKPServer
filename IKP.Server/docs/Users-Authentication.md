# Users / Authentication — initial implementation

## Scope

This stage introduces the authentication foundation in the `auth` bounded context:

- `auth.Users`: account identity, normalized username/email, activation, soft deletion and lockout metadata. `IsBootstrapAccount` identifies the first account for the upcoming RBAC bootstrap; it is provenance metadata, not an authorization grant.
- `auth.UserCredentials`: method-specific password hashes. Password hashes are deliberately not stored on `Users` and credentials are excluded from automatic audit logging.
- `auth.UserSessions`: refresh-token hashes, expiry/revocation, rotation chain and client metadata.
- `auth_lookup.AuthenticationMethods`: localized lookup definitions. `PASSWORD` is seeded; the schema allows additional methods later.
- JWT bearer access tokens, rotating opaque refresh tokens, password change, failed-login lockout and a one-time initial-account bootstrap.
- `AuditSaveChangesInterceptor` now attributes changes to the authenticated user when an HTTP request has a valid `sub` claim. Diagnostic occurrences also capture `UserId` and `SessionId` from claims.

Role/permission tables and admin user-management endpoints are intentionally not included yet. They are the next stage. Existing non-auth controllers are not globally protected in this stage; do not expose this intermediate build as a production-secured system until role/permission authorization is implemented.

## Configure local secrets before starting or running EF tools

The PostgreSQL connection string, signing key and bootstrap secret must not be committed to `appsettings.json`. The existing connection string was removed from the archive; configure it from the value in your private local configuration before starting the API or running EF tools.

Run these commands from the solution directory in PowerShell:

```powershell
dotnet user-secrets init --project IKP.Api

$bytes = New-Object byte[] 48
$rng = [System.Security.Cryptography.RandomNumberGenerator]::Create()
$rng.GetBytes($bytes)
$rng.Dispose()
$signingKey = [Convert]::ToBase64String($bytes)
$bootstrapSecret = [Guid]::NewGuid().ToString("N")

dotnet user-secrets set "Authentication:SigningKey" $signingKey --project IKP.Api
dotnet user-secrets set "Authentication:BootstrapSecret" $bootstrapSecret --project IKP.Api
# Use your current private PostgreSQL connection string here:
dotnet user-secrets set "ConnectionStrings:Default" "Host=YOUR_HOST;Port=5432;Database=YOUR_DATABASE;Username=YOUR_USER;Password=YOUR_PASSWORD" --project IKP.Api
```

Keep `$bootstrapSecret` for the first request only. Do not put secrets into source control. For a deployed service, provide them through the deployment secret manager or environment variables (`ConnectionStrings__Default`, `Authentication__SigningKey`, `Authentication__BootstrapSecret`).

## Apply the migration

```powershell
dotnet build
dotnet ef database update --project IKP.Infrastructure --startup-project IKP.Api
```

The migration is `20261015164000_AddAuthenticationFoundation`. It creates `auth` and `auth_lookup`, the four tables, indexes and the `PASSWORD` lookup record. It does not modify or reset earlier migrations.

## First account bootstrap

Bootstrap works only while the `auth.Users` table has no rows. It is permanently disabled after the first account is created. Send the configured bootstrap secret over a trusted local/development connection only.

```http
POST /api/auth/bootstrap
Content-Type: application/json
```

```json
{
  "bootstrapSecret": "THE_VALUE_FROM_USER_SECRETS",
  "userName": "admin",
  "email": "admin@example.invalid",
  "displayName": "Initial administrator",
  "password": "A-long-unique-password-12"
}
```

The response contains `accessToken`, `refreshToken`, `expiresIn` and a public user profile. Never log either token. The first account is not yet assigned an authorization role because RBAC is a separate stage. Its `IsBootstrapAccount` marker lets the next migration assign the system-administrator role without guessing which account was bootstrapped.

## Smoke tests

### 1. Login

```http
POST /api/auth/login
Content-Type: application/json
```

```json
{
  "userNameOrEmail": "admin",
  "password": "THE_PASSWORD_USED_DURING_BOOTSTRAP"
}
```

Expected: `200 OK` with access and refresh tokens. Invalid credentials return the same generic `401` response regardless of whether the account exists.

### 2. Current profile

```http
GET /api/auth/me
Authorization: Bearer ACCESS_TOKEN
```

Expected: `200 OK` with `id`, `userName`, `email` and `displayName`.

### 3. Refresh

```http
POST /api/auth/refresh
Content-Type: application/json
```

```json
{
  "refreshToken": "CURRENT_REFRESH_TOKEN"
}
```

Expected: `200 OK` with a new refresh token. The old refresh token is revoked. Reuse of a revoked token is treated as replay and revokes the account's active refresh sessions, so perform that negative test last.

### 4. Logout

```http
POST /api/auth/logout
Content-Type: application/json
```

```json
{
  "refreshToken": "CURRENT_REFRESH_TOKEN"
}
```

Expected: `204 No Content`; subsequent refresh with that token returns `401`.

### 5. Database checks

```sql
SELECT "Id", "UserName", "NormalizedUserName", "Email", "IsActive", "FailedLoginCount", "LockoutEndAt", "LastLoginAt"
FROM auth."Users"
ORDER BY "CreatedAt";

SELECT "Id", "UserId", "AuthenticationMethodId", "IsActive", "MustChangePassword", "CreatedAt", "LastUsedAt"
FROM auth."UserCredentials";

SELECT "Id", "UserId", "CreatedAt", "ExpiresAt", "LastUsedAt", "RevokedAt", "ReplacedBySessionId", "IpAddress", "UserAgent"
FROM auth."UserSessions"
ORDER BY "CreatedAt" DESC;

SELECT "Code", "NameKey", "IsActive", "IsSystem"
FROM auth_lookup."AuthenticationMethods"
ORDER BY "SortOrder";
```

There must be no plaintext password or refresh token in these tables. `SecretHash` is an Identity password hash; `RefreshTokenHash` is SHA-256 of the opaque refresh token.

## Security notes and next stage

- Access token default lifetime is 10 minutes; refresh token default lifetime is 30 days.
- Password minimum length defaults to 12 characters.
- Five consecutive failed password attempts trigger a 15-minute lockout by default.
- Bootstrap, login, refresh and logout endpoints are rate-limited to 30 requests per minute per client IP in each API instance.
- Refresh tokens rotate. Reuse of an already revoked token revokes all active refresh sessions for that user.
- Logout and password change revoke refresh sessions. Already-issued JWT access tokens can remain valid until their short expiry; immediate per-request session revocation is not implemented.
- The one-time bootstrap endpoint is not a user-registration endpoint.
- Next: `Roles`, `Permissions`, `UserRoles`, `RolePermissions`, `UserPermissions` (if direct grants are required), authorization policies, and administrator-only user provisioning/deactivation.
