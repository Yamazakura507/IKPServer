using IKP.Domain.Common.Monitoring;
using IKP.Domain.Entities.Monitoring;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds;

public static class MonitoringSeed
{
    private const uint BaseIndex = 9;

    private static readonly Guid HealthRetentionId = BaseIndex.GetGuidForLevel(1, 1);

    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RetentionPolicy>().HasData(new RetentionPolicy
        {
            Id = HealthRetentionId, 
            AreaCode = PolicyCodes.MonitoringHealth, 
            RetentionDays = 30, 
            IsActive = true,
            CreatedAt = DateTime.UnixEpoch
        });
    }
}
