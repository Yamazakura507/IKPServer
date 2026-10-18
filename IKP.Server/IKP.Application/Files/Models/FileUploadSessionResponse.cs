namespace IKP.Application.Files.Models;

/// <summary>
/// Describes an in-progress file upload session.
/// </summary>
public sealed class FileUploadSessionResponse
{
    /// <summary>
    /// Gets the upload session identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the expected content length, when supplied.
    /// </summary>
    public long? ExpectedSize { get; init; }

    /// <summary>
    /// Gets the number of bytes already received.
    /// </summary>
    public long BytesReceived { get; init; }

    /// <summary>
    /// Gets the next byte offset accepted by the server.
    /// </summary>
    public long NextOffset { get; init; }

    /// <summary>
    /// Gets the UTC expiration timestamp.
    /// </summary>
    public DateTime ExpiresAt { get; init; }
}
