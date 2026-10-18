namespace IKP.Application.Support.Models;

/// <summary>
/// Defines an internal support note.
/// </summary>
public sealed class AddSupportInternalNoteRequest
{
    /// <summary>
    /// Gets or sets the internal note content.
    /// </summary>
    public string Message { get; set; } = null!;

    /// <summary>
    /// Gets or sets existing file identifiers to attach.
    /// </summary>
    public IReadOnlyCollection<Guid>? AttachmentFileIds { get; set; }
}
