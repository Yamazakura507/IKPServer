namespace IKP.Domain.Common.Support;

/// <summary>
/// Contains stable support ticket status codes.
/// </summary>
public static class SupportTicketStatusCodes
{
    /// <summary>
    /// New ticket awaiting processing.
    /// </summary>
    public const string New = "NEW";

    /// <summary>
    /// Ticket is actively being processed.
    /// </summary>
    public const string Open = "OPEN";

    /// <summary>
    /// Ticket is being actively worked on.
    /// </summary>
    public const string InProgress = "IN_PROGRESS";

    /// <summary>
    /// Processing is waiting for the requester.
    /// </summary>
    public const string WaitingForUser = "WAITING_FOR_USER";

    /// <summary>
    /// Ticket has been resolved.
    /// </summary>
    public const string Resolved = "RESOLVED";

    /// <summary>
    /// Ticket is closed.
    /// </summary>
    public const string Closed = "CLOSED";
}
