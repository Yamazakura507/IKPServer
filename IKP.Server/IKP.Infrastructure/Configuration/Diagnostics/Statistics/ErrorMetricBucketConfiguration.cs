using IKP.Domain.Entities.Diagnostics.Statistics;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Statistics
{
    public class ErrorMetricBucketConfiguration : IEntityTypeConfiguration<ErrorMetricBucket>
    {
        public void Configure(EntityTypeBuilder<ErrorMetricBucket> builder)
        {
            builder.ToTable("ErrorMetricBuckets", DatabaseSchemas.Diagnostics);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ErrorGroupId).IsRequired();
            builder.Property(x => x.BucketStart).IsRequired();
            builder.Property(x => x.BucketDurationMinutes).IsRequired();
            builder.Property(x => x.BucketTypeId).IsRequired();
            builder.Property(x => x.Occurrences).IsRequired();
            builder.Property(x => x.UniqueUsers).IsRequired();
            builder.Property(x => x.FirstOccurrenceAt);
            builder.Property(x => x.LastOccurrenceAt);
            builder.Property(x => x.CalculatedAt).IsRequired();

            builder.HasOne(x => x.ErrorGroup).WithMany().HasForeignKey(x => x.ErrorGroupId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.BucketTypeDefinition).WithMany().HasForeignKey(x => x.BucketTypeId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.ErrorGroupId,
                x.BucketTypeId,
                x.BucketStart,
                x.BucketDurationMinutes
            }).IsUnique();
            builder.HasIndex(x => new
            {
                x.ErrorGroupId,
                x.BucketStart
            });
            builder.HasIndex(x => new
            {
                x.BucketTypeId,
                x.BucketStart
            });
            builder.HasIndex(x => x.CalculatedAt);
        }
    }
}
