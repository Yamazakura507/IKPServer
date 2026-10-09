using IKP.Domain.Entities.Diagnostics.Statistics;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Statistics
{
    public class ErrorStatisticsConfiguration : IEntityTypeConfiguration<ErrorStatistics>
    {
        public void Configure(EntityTypeBuilder<ErrorStatistics> builder)
        {
            builder.ToTable("ErrorStatistics", DatabaseSchemas.Diagnostics);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ErrorGroupId).IsRequired();
            builder.Property(x => x.TotalOccurrences).IsRequired();
            builder.Property(x => x.UniqueUsers).IsRequired();
            builder.Property(x => x.AffectedUserRatio).IsRequired().HasPrecision(5, 2);
            builder.Property(x => x.OccurrencesLastHour).IsRequired();
            builder.Property(x => x.OccurrencesLastDay).IsRequired();
            builder.Property(x => x.OccurrencesLast7Days).IsRequired();
            builder.Property(x => x.UsersLastHour).IsRequired();
            builder.Property(x => x.UsersLastDay).IsRequired();
            builder.Property(x => x.UsersLast7Days).IsRequired();
            builder.Property(x => x.GrowthRate).IsRequired().HasPrecision(9, 2);
            builder.Property(x => x.CalculatedAt).IsRequired();

            builder.HasOne(x => x.ErrorGroup).WithOne(x => x.Statistics).HasForeignKey<ErrorStatistics>(x => x.ErrorGroupId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.ErrorGroupId).IsUnique();
            builder.HasIndex(x => x.CalculatedAt);
        }
    }
}
