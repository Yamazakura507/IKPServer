namespace IKP.Application.Support.Exceptions;

/// <summary>
/// Represents a controlled support operation failure.
/// </summary>
public sealed class SupportOperationException : Exception
{
    /// <summary>Initializes a support operation exception.</summary>
    public SupportOperationException(int statusCode, string code, string message) : base(message)
    {
        StatusCode = statusCode;
        Code = code;
    }

    /// <summary>
    /// Gets the HTTP status code.
    /// </summary>
    public int StatusCode { get; }
    /// <summary>
    /// Gets the stable application error code.
    /// </summary>
    public string Code { get; }
}
