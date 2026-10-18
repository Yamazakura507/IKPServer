namespace IKP.Application.Files.Models;

/// <summary>
/// Configures the server-side file storage subsystem.
/// </summary>
public sealed class FileStorageOptions
{
    /// <summary>
    /// Gets or sets the local repository root.
    /// </summary>
    public string RootPath { get; set; } = "/srv/storage";

    /// <summary>
    /// Gets or sets the maximum accepted file size in bytes.
    /// </summary>
    public long MaximumFileSizeBytes { get; set; } = 5L * 1024L * 1024L * 1024L;

    /// <summary>
    /// Gets or sets the lifetime of incomplete upload sessions in minutes.
    /// </summary>
    public int UploadSessionLifetimeMinutes { get; set; } = 120;

    /// <summary>
    /// Gets or sets the physical storage provider code.
    /// </summary>
    public string ProviderCode { get; set; } = "LOCAL";

    /// <summary>
    /// Gets or sets the size of the asynchronous copy buffer.
    /// </summary>
    public int CopyBufferSizeBytes { get; set; } = 1024 * 1024;
}
