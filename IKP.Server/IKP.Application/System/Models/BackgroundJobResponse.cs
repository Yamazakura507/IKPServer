namespace IKP.Application.System.Models;

/// <summary>
/// Represents a background job lifecycle snapshot.
/// </summary>
public sealed class BackgroundJobResponse
{
    public Guid Id { get; set; }
    public string JobType { get; set; } = null!;
    public string? Payload { get; set; }
    public string StatusCode { get; set; } = null!;
    public int AttemptCount { get; set; }
    public DateTime? ScheduledAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? NextAttemptAt { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime CreatedAt { get; set; }
}
