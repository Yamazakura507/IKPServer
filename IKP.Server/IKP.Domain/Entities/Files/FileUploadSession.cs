using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Files;

/// <summary>
/// Tracks an in-progress streaming upload so incomplete physical files can be expired safely.
/// </summary>
public sealed class FileUploadSession : Entity
{
    /// <summary>
    /// Gets or sets the temporary storage key.
    /// </summary>
    public string StorageKey { get; set; } = null!;

    /// <summary>
    /// Gets or sets the original file name supplied by the client.
    /// </summary>
    public string OriginalFileName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the path file name supplied by the client.
    /// </summary>
    public string? PathAreaFileName { get; set; } = null;

    /// <summary>
    /// Gets or sets the content type supplied by the client.
    /// </summary>
    public string ContentType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the expected content length when known.
    /// </summary>
    public long? ExpectedSize { get; set; }

    /// <summary>
    /// Gets or sets the number of bytes already persisted to temporary storage.
    /// </summary>
    public long BytesReceived { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp of the last upload activity.
    /// </summary>
    public DateTime LastActivityAt { get; set; }

    /// <summary>
    /// Gets or sets the UTC expiration timestamp.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Gets or sets the user that created the upload session.
    /// </summary>
    public Guid? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets whether the upload has been finalized.
    /// </summary>
    public bool IsCompleted { get; set; }
}
