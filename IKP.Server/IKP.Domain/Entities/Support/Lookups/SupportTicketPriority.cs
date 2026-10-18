using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Support.Lookups;

/// <summary>
/// Defines a support ticket priority.
/// </summary>
public sealed class SupportTicketPriority : FullLookupLocalized
{
    /// <summary>
    /// Gets or sets the numeric priority weight used for ordering.
    /// </summary>
    public int Weight { get; set; }
}
