namespace IKP.Application.Support.Models;

/// <summary>
/// Defines a support ticket assignment.
/// </summary>
public sealed class AssignSupportTicketRequest
{
    /// <summary>
    /// Gets or sets the target support user identifier. Null removes the current assignment.
    /// </summary>
    public Guid? AssignedToUserId { get; set; }
}
