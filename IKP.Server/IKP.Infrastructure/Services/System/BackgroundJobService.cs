using IKP.Application.System.Interfaces;
using IKP.Application.System.Models;
using IKP.Domain.Common.Codes.System;
using IKP.Domain.Entities.System;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.System;

/// <summary>
/// Implements durable background job persistence.
/// </summary>
public sealed class BackgroundJobService(AppDbContext db) : IBackgroundJobService
{
    public async ValueTask<BackgroundJobResponse> EnqueueAsync(string jobType, string? payload, DateTime? scheduledAt, Guid? userId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(jobType)) throw new ArgumentException("Job type is required.", nameof(jobType));

        BackgroundJob job = new()
        {
            JobType = jobType.Trim(), 
            Payload = payload, 
            StatusCode = BackgroundJobStatusCodes.Queued,
            ScheduledAt = scheduledAt,
            NextAttemptAt = scheduledAt ?? DateTime.UtcNow, 
            CreatedBy = userId, 
            CreatedAt = DateTime.UtcNow
        };

        db.Set<BackgroundJob>().Add(job);
        await db.SaveChangesAsync(cancellationToken);

        return ToResponse(job);
    }

    public async ValueTask<BackgroundJobResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default) =>
        await db.Set<BackgroundJob>().AsNoTracking().Where(x => x.Id == id).Select(x => ToResponse(x)).SingleOrDefaultAsync(cancellationToken);

    private static BackgroundJobResponse ToResponse(BackgroundJob backgroundJob) => new()
    { 
        Id = backgroundJob.Id, 
        JobType = backgroundJob.JobType, 
        Payload = backgroundJob.Payload, 
        StatusCode = backgroundJob.StatusCode, 
        AttemptCount = backgroundJob.AttemptCount, 
        ScheduledAt = backgroundJob.ScheduledAt, 
        StartedAt = backgroundJob.StartedAt, 
        CompletedAt = backgroundJob.CompletedAt, 
        NextAttemptAt = backgroundJob.NextAttemptAt, 
        ErrorMessage = backgroundJob.ErrorMessage, 
        CreatedAt = backgroundJob.CreatedAt 
    };
}
