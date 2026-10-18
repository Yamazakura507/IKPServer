using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Support.Lookups;

/// <summary>
/// Defines a support ticket workflow status.
/// </summary>
public sealed class SupportTicketStatus : FullLookupLocalized
{
    /// <summary>
    /// Gets or sets whether the status represents a terminal state.
    /// </summary>
    public bool IsTerminal { get; set; }
}
