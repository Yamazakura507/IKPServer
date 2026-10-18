using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Files;

namespace IKP.Domain.Entities.Support;

/// <summary>
/// Links a support message to an existing logical file.
/// </summary>
public sealed class SupportTicketMessageAttachment : Entity
{
    /// <summary>
    /// Gets or sets the support message identifier.
    /// </summary>
    public Guid MessageId { get; set; }

    /// <summary>
    /// Gets or sets the logical file identifier.
    /// </summary>
    public Guid FileId { get; set; }

    /// <summary>
    /// Gets the support message.
    /// </summary>
    public SupportTicketMessage Message { get; set; } = null!;

    /// <summary>
    /// Gets the logical file.
    /// </summary>
    public StoredFile File { get; set; } = null!;
}
