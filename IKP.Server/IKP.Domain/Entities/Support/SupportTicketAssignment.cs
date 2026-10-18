using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Audit;
using IKP.Domain.Entities.Auth.Users;

namespace IKP.Domain.Entities.Support;

/// <summary>
/// Records the assignment history of a support ticket.
/// </summary>
public sealed class SupportTicketAssignment : Entity, IAuditable
{
    /// <summary>
    /// Gets or sets the ticket identifier.
    /// </summary>
    public Guid TicketId { get; set; }

    /// <summary>
    /// Gets or sets the assigned user identifier.
    /// </summary>
    public Guid? AssignedToUserId { get; set; }

    /// <summary>
    /// Gets or sets who performed the assignment.
    /// </summary>
    public Guid AssignedByUserId { get; set; }

    /// <summary>
    /// Gets or sets when the assignment started.
    /// </summary>
    public DateTime AssignedAt { get; set; }

    /// <summary>
    /// Gets or sets when the assignment ended.
    /// </summary>
    public DateTime? UnassignedAt { get; set; }

    /// <inheritdoc />
    public DateTime CreatedAt { get; set; }
    /// <inheritdoc />
    public Guid? CreatedBy { get; set; }
    /// <inheritdoc />
    public DateTime? UpdatedAt { get; set; }
    /// <inheritdoc />
    public Guid? UpdatedBy { get; set; }

    /// <summary>
    /// Gets the parent ticket.
    /// </summary>
    public SupportTicket Ticket { get; set; } = null!;

    /// <summary>
    /// Gets the assigned user.
    /// </summary>
    public AuthUser? AssignedToUser { get; set; }

    /// <summary>
    /// Gets the user who changed the assignment.
    /// </summary>
    public AuthUser AssignedByUser { get; set; } = null!;
}
