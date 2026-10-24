using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.System;

/// <summary>
/// Stores a durable background operation and its execution state.
/// </summary>
public sealed class BackgroundJob : Entity
{
    public string JobType { get; set; } = null!;
    public string? Payload { get; set; }
    public string StatusCode { get; set; } = null!;
    public int AttemptCount { get; set; }
    public DateTime? ScheduledAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? NextAttemptAt { get; set; }
    public string? ErrorMessage { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
