using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Audit;
using IKP.Domain.Entities.Auth.Users;

namespace IKP.Domain.Entities.Support;

/// <summary>
/// Represents a message or internal note within a support ticket.
/// </summary>
public sealed class SupportTicketMessage : Entity, IAuditable, ISoftDelete
{
    /// <summary>
    /// Gets or sets the ticket identifier.
    /// </summary>
    public Guid TicketId { get; set; }

    /// <summary>
    /// Gets or sets the author user identifier.
    /// </summary>
    public Guid AuthorUserId { get; set; }

    /// <summary>
    /// Gets or sets whether the message is an internal note.
    /// </summary>
    public bool IsInternal { get; set; }

    /// <summary>
    /// Gets or sets the message text.
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Gets or sets the UTC creation timestamp used for chronological ordering.
    /// </summary>
    public DateTime SentAt { get; set; }

    /// <inheritdoc />
    public DateTime CreatedAt { get; set; }
    /// <inheritdoc />
    public Guid? CreatedBy { get; set; }
    /// <inheritdoc />
    public DateTime? UpdatedAt { get; set; }
    /// <inheritdoc />
    public Guid? UpdatedBy { get; set; }
    /// <inheritdoc />
    public bool IsDeleted { get; set; }
    /// <inheritdoc />
    public DateTime? DeletedAt { get; set; }
    /// <inheritdoc />
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// Gets the parent ticket.
    /// </summary>
    public SupportTicket Ticket { get; set; } = null!;

    /// <summary>
    /// Gets the author.
    /// </summary>
    public AuthUser AuthorUser { get; set; } = null!;

    /// <summary>
    /// Gets message attachments.
    /// </summary>
    public ICollection<SupportTicketMessageAttachment> Attachments { get; set; } = new List<SupportTicketMessageAttachment>();
}
