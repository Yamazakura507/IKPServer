namespace IKP.Application.Support.Models;

/// <summary>
/// Describes a support ticket message.
/// </summary>
public sealed class SupportTicketMessageResponse
{
    /// <summary>
    /// Gets the message identifier.
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Gets the ticket identifier.
    /// </summary>
    public Guid TicketId { get; init; }
    
    /// <summary>
    /// Gets the author identifier.
    /// </summary>
    public Guid AuthorUserId { get; init; }
    
    /// <summary>
    /// Gets whether the message is internal.
    /// </summary>
    public bool IsInternal { get; init; }
    
    /// <summary>
    /// Gets the message content.
    /// </summary>
    public string Content { get; init; } = null!;
    
    /// <summary>
    /// Gets the sent timestamp.
    /// </summary>
    public DateTime SentAt { get; init; }
    
    /// <summary>
    /// Gets attached logical file identifiers.
    /// </summary>
    public IReadOnlyList<Guid> AttachmentFileIds { get; init; } = [];
}
