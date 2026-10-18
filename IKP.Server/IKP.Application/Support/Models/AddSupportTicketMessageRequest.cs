namespace IKP.Application.Support.Models;

/// <summary>
/// Defines a public support ticket message.
/// </summary>
public sealed class AddSupportTicketMessageRequest
{
    /// <summary>
    /// Gets or sets the message content.
    /// </summary>
    public string Message { get; set; } = null!;

    /// <summary>
    /// Gets or sets existing file identifiers to attach.
    /// </summary>
    public IReadOnlyCollection<Guid>? AttachmentFileIds { get; set; }
}
