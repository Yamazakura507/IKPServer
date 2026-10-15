using System.Security.Claims;
using IKP.Application.Common.Interfaces;

namespace IKP.Api.Security;

/// <summary>
/// Reads the current user and session identifiers from the active HTTP request.
/// </summary>
public sealed class HttpCurrentUserContext : ICurrentUserContext
{
    private readonly IHttpContextAccessor httpContextAccessor;

    /// <summary>
    /// Initializes the current-user context.
    /// </summary>
    public HttpCurrentUserContext(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc />
    public Guid? UserId => ParseGuid(Principal?.FindFirst("sub")?.Value ?? Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    /// <inheritdoc />
    public Guid? SessionId => ParseGuid(Principal?.FindFirst("sid")?.Value);

    private ClaimsPrincipal? Principal => httpContextAccessor.HttpContext?.User;

    private static Guid? ParseGuid(string? value) => Guid.TryParse(value, out Guid id) ? id : null;
}
