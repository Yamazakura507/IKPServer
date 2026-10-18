namespace IKP.Application.Support.Models;

/// <summary>
/// Defines a support ticket status change.
/// </summary>
public sealed class SetSupportTicketStatusRequest
{
    /// <summary>
    /// Gets or sets the target status identifier.
    /// </summary>
    public Guid StatusId { get; set; }
}
