using IKP.Application.Monitoring.Interfaces;
using IKP.Domain.Common.Monitoring;
using IKP.Domain.Entities.Monitoring;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.Monitoring;

public sealed class MonitoringService(AppDbContext db) : IMonitoringService
{
    public async ValueTask<HealthSnapshot> CheckAsync(string component, CancellationToken cancellationToken = default)
    {
        DateTime started = DateTime.UtcNow; 
        string status = MonitoringCodes.Healthy; 
        string? details = null;

        try 
        { 
            bool canConnect = await db.Database.CanConnectAsync(cancellationToken);
            
            if (!canConnect)
            {
                status = MonitoringCodes.Unhealthy;
                details = "Database connection check returned false.";
            } 
        }
        catch(Exception ex) 
        { 
            status = MonitoringCodes.Unhealthy; 
            details = ex.Message;
        }

        HealthSnapshot shapshot = new()
        { 
            Component = component,
            StatusCode = status, 
            DurationMilliseconds = (DateTime.UtcNow - started).TotalMilliseconds, 
            Details = details, 
            CheckedAt = DateTime.UtcNow
        };

        db.HealthSnapshots.Add(shapshot); 
        await db.SaveChangesAsync(cancellationToken);
        
        return shapshot;
    }

    public async ValueTask<IReadOnlyList<HealthSnapshot>> GetRecentHealthAsync(int limit, CancellationToken cancellationToken=default) => 
        await db.HealthSnapshots.AsNoTracking().OrderByDescending(x => x.CheckedAt).Take(Math.Clamp(limit, 1, 500)).ToListAsync(cancellationToken);

    public async ValueTask<int> ApplyRetentionAsync(CancellationToken cancellationToken=default)
    {
        int total = 0; 
        DateTime now = DateTime.UtcNow;

        foreach(RetentionPolicy p in await db.RetentionPolicies.Where(x => x.IsActive && x.RetentionDays >= 1).ToListAsync(cancellationToken))
        {
            DateTime cutoff = now.AddDays(-p.RetentionDays);

            if(p.AreaCode == PolicyCodes.MonitoringHealth) total += await db.HealthSnapshots.Where(x => x.CheckedAt < cutoff).ExecuteDeleteAsync(cancellationToken);

            p.LastRunAt = now;
        }

        await db.SaveChangesAsync(cancellationToken); 
        
        return total;
    }
}
