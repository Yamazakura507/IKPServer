namespace IKP.Application.Files.Exceptions;

/// <summary>
/// Represents a controlled file-storage operation failure with an HTTP status code.
/// </summary>
public sealed class FileStorageException : Exception
{
    /// <summary>
    /// Initializes a file-storage exception.
    /// </summary>
    public FileStorageException(int statusCode, string code, string message) : base(message)
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
