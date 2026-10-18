namespace IKP.Application.Files.Models;

/// <summary>
/// Defines logical metadata for a new file upload.
/// </summary>
public sealed class CreateFileUploadRequest
{
    /// <summary>
    /// Gets or sets the client file name.
    /// </summary>
    public string FileName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the client media type.
    /// </summary>
    public string? ContentType { get; set; }

    /// <summary>
    /// Gets or sets the expected content length when known.
    /// </summary>
    public long? ExpectedSize { get; set; }
}
