namespace IKP.Application.Support.Models;

/// <summary>
/// Defines mutable support ticket metadata.
/// </summary>
public sealed class UpdateSupportTicketRequest
{
    /// <summary>
    /// Gets or sets the subject.
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
}
