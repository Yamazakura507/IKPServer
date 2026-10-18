using IKP.Application.Files.Interfaces;
using IKP.Application.Files.Models;
using IKP.Domain.Entities.Files;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IKP.Infrastructure.Services.Files;

/// <summary>
/// Removes expired incomplete upload sessions and their temporary physical objects.
/// </summary>
public sealed class FileUploadCleanupWorker : BackgroundService
{
    private readonly IServiceScopeFactory scopeFactory;
    private readonly FileStorageOptions options;
    private readonly ILogger<FileUploadCleanupWorker> logger;

    /// <summary>
    /// Initializes the upload cleanup worker.
    /// </summary>
    public FileUploadCleanupWorker(IServiceScopeFactory scopeFactory, IOptions<FileStorageOptions> options, ILogger<FileUploadCleanupWorker> logger)
    {
        this.scopeFactory = scopeFactory;
        this.options = options.Value;
        this.logger = logger;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CleanupAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "File upload cleanup failed.");
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }

    private async Task CleanupAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = scopeFactory.CreateScope();
        AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        IFileStorageProvider storage = scope.ServiceProvider.GetRequiredService<IFileStorageProvider>();
        DateTime cutoff = DateTime.UtcNow;

        List<FileUploadSession> sessions = await db.FileUploadSessions.Where(x => !x.IsCompleted && x.ExpiresAt <= cutoff).Take(100).ToListAsync(cancellationToken);
        foreach (FileUploadSession session in sessions)
        {
            try
            {
                await storage.DeleteAsync(session.StorageKey, CancellationToken.None);
                db.FileUploadSessions.Remove(session);
                await db.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                logger.LogWarning(exception, "Failed to clean expired upload session {UploadId}.", session.Id);
            }
        }
    }
}
