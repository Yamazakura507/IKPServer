namespace IKP.Application.Support.Models;

/// <summary>
/// Describes a support ticket.
/// </summary>
public sealed class SupportTicketResponse
{
    /// <summary>
    /// Gets the ticket identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the human-facing ticket number.
    /// </summary>
    public long Number { get; init; }

    /// <summary>
    /// Gets the subject.
    /// </summary>
    public string Subject { get; init; } = null!;

    /// <summary>
    /// Gets the creator identifier.
    /// </summary>
    public Guid CreatedByUserId { get; init; }

    /// <summary>
    /// Gets the category identifier.
    /// </summary>
    public Guid CategoryId { get; init; }

    /// <summary>
    /// Gets the category code.
    /// </summary>
    public string CategoryCode { get; init; } = null!;

    /// <summary>
    /// Gets the priority identifier.
    /// </summary>
    public Guid PriorityId { get; init; }

    /// <summary>
    /// Gets the priority code.
    /// </summary>
    public string PriorityCode { get; init; } = null!;

    /// <summary>
    /// Gets the status identifier.
    /// </summary>
    public Guid StatusId { get; init; }

    /// <summary>
    /// Gets the status code.
    /// </summary>
    public string StatusCode { get; init; } = null!;

    /// <summary>
    /// Gets the assigned user identifier.
    /// </summary>
    public Guid? AssignedToUserId { get; init; }

    /// <summary>
    /// Gets the latest message timestamp.
    /// </summary>
    public DateTime LastMessageAt { get; init; }

    /// <summary>
    /// Gets the resolution timestamp.
    /// </summary>
    public DateTime? ResolvedAt { get; init; }

    /// <summary>
    /// Gets the closure timestamp.
    /// </summary>
    public DateTime? ClosedAt { get; init; }

    /// <summary>
    /// Gets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets the update timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; init; }
}
