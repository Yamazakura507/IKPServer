namespace IKP.Application.Common.Interfaces;

/// <summary>Exposes the authenticated user identifier to server-side infrastructure.</summary>
public interface ICurrentUserContext
{
    /// <summary>Gets the current authenticated user identifier, or null for anonymous/background work.</summary>
    Guid? UserId { get; }

    /// <summary>Gets the current authentication session identifier, or null when unavailable.</summary>
    Guid? SessionId { get; }
}
