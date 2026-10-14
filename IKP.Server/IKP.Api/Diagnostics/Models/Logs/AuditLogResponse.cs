namespace IKP.Api.Diagnostics.Models.Logs;

/// <summary>Represents a compact audit log list/detail response.</summary>
public sealed class AuditLogResponse
{
    /// <summary>Gets the audit log identifier.</summary>
    public Guid Id { get; init; }
    /// <summary>Gets the audited entity schema.</summary>
    public string EntitySchema { get; init; } = string.Empty;
    /// <summary>Gets the audited entity type.</summary>
    public string EntityType { get; init; } = string.Empty;
    /// <summary>Gets the audited entity identifier.</summary>
    public Guid EntityId { get; init; }
    /// <summary>Gets the action identifier.</summary>
    public Guid ActionId { get; init; }
    /// <summary>Gets the action code.</summary>
    public string ActionCode { get; init; } = string.Empty;
    /// <summary>Gets the optional user identifier.</summary>
    public Guid? UserId { get; init; }
    /// <summary>Gets the identifier of the audit event that this restore event cancels.</summary>
    public Guid? RestorLogId { get; init; }
    /// <summary>Gets the creation time.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Gets the changes recorded by the audit event.</summary>
    public IReadOnlyList<AuditLogChangeResponse> Changes { get; init; } = [];
}

/// <summary>Represents one audited property change.</summary>
public sealed class AuditLogChangeResponse
{
    /// <summary>Gets the change identifier.</summary>
    public Guid Id { get; init; }
    /// <summary>Gets the property name.</summary>
    public string PropertyName { get; init; } = string.Empty;
    /// <summary>Gets the property type.</summary>
    public string PropertyType { get; init; } = string.Empty;
    /// <summary>Gets the serialized old value.</summary>
    public string? OldValue { get; init; }
    /// <summary>Gets the serialized new value.</summary>
    public string? NewValue { get; init; }
}
