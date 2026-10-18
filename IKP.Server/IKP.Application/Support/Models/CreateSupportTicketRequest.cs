namespace IKP.Application.Support.Models;

/// <summary>
/// Defines a new support ticket.
/// </summary>
public sealed class CreateSupportTicketRequest
{
    /// <summary>
    /// Gets or sets the ticket subject.
    /// </summary>
    public string Subject { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the category identifier.
    /// </summary>
    public Guid CategoryId { get; set; }
    
    /// <summary>
    /// Gets or sets the priority identifier.
    /// </summary>
    public Guid PriorityId { get; set; }
    
    /// <summary>
    /// Gets or sets the initial public message.
    /// </summary>
    public string Message { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets existing file identifiers to attach to the initial message.
    /// </summary>
    public IReadOnlyCollection<Guid>? AttachmentFileIds { get; set; }
}
