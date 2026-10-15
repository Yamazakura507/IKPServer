namespace IKP.Application.Auth.Exceptions;

/// <summary>
/// Represents a safe authentication error that can be returned to an API caller.
/// </summary>
public sealed class AuthOperationException : Exception
{
    /// <summary>Initializes a new instance.</summary>
    /// <param name="statusCode">HTTP status code.</param>
    /// <param name="code">Stable machine-readable error code.</param>
    /// <param name="message">Safe message suitable for an API response.</param>
    public AuthOperationException(int statusCode, string code, string message) : base(message)
    {
        StatusCode = statusCode;
        Code = code;
    }

    /// <summary>
    /// Gets the HTTP status code.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Gets the stable error code.
    /// </summary>
    public string Code { get; }
}
