using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Audit;
using IKP.Domain.Entities.Auth.Users;
using IKP.Domain.Entities.Support.Lookups;

namespace IKP.Domain.Entities.Support;

/// <summary>
/// Represents a support conversation opened by an authenticated user.
/// </summary>
public sealed class SupportTicket : Entity, IAuditable, ISoftDelete
{
    /// <summary>
    /// Gets or sets the stable human-facing ticket number.
    /// </summary>
    public long Number { get; set; }

    /// <summary>
    /// Gets or sets the ticket subject.
    /// </summary>
    public string Subject { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who created the ticket.
    /// </summary>
    public Guid CreatedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the category identifier.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the priority identifier.
    /// </summary>
    public Guid PriorityId { get; set; }

    /// <summary>
    /// Gets or sets the current status identifier.
    /// </summary>
    public Guid StatusId { get; set; }

    /// <summary>
    /// Gets or sets the currently assigned support user.
    /// </summary>
    public Guid? AssignedToUserId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the most recent message.
    /// </summary>
    public DateTime LastMessageAt { get; set; }

    /// <summary>
    /// Gets or sets when the ticket was resolved.
    /// </summary>
    public DateTime? ResolvedAt { get; set; }

    /// <summary>
    /// Gets or sets the user who resolved the ticket.
    /// </summary>
    public Guid? ResolvedByUserId { get; set; }

    /// <summary>
    /// Gets or sets when the ticket was closed.
    /// </summary>
    public DateTime? ClosedAt { get; set; }

    /// <summary>
    /// Gets or sets the user who closed the ticket.
    /// </summary>
    public Guid? ClosedByUserId { get; set; }

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
    /// Gets the ticket creator.
    /// </summary>
    public AuthUser CreatedByUser { get; set; } = null!;

    /// <summary>
    /// Gets the current assignee.
    /// </summary>
    public AuthUser? AssignedToUser { get; set; }

    /// <summary>
    /// Gets the ticket category.
    /// </summary>
    public SupportTicketCategory Category { get; set; } = null!;

    /// <summary>
    /// Gets the ticket priority.
    /// </summary>
    public SupportTicketPriority Priority { get; set; } = null!;

    /// <summary>
    /// Gets the ticket status.
    /// </summary>
    public SupportTicketStatus Status { get; set; } = null!;

    /// <summary>
    /// Gets the messages belonging to this ticket.
    /// </summary>
    public ICollection<SupportTicketMessage> Messages { get; set; } = new List<SupportTicketMessage>();

    /// <summary>
    /// Gets the assignment history.
    /// </summary>
    public ICollection<SupportTicketAssignment> Assignments { get; set; } = new List<SupportTicketAssignment>();
}
