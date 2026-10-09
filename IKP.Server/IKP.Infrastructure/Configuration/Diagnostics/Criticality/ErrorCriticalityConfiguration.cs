using IKP.Domain.Entities.Diagnostics.Criticality;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Criticality
{
    public class ErrorCriticalityConfiguration : IEntityTypeConfiguration<ErrorCriticality>
    {
        public void Configure(EntityTypeBuilder<ErrorCriticality> builder)
        {
            builder.ToTable("ErrorCriticalities", DatabaseSchemas.Diagnostics);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ErrorGroupId).IsRequired();
            builder.Property(x => x.AutomaticScore).IsRequired().HasPrecision(5, 2);
            builder.Property(x => x.ManualScore).HasPrecision(5, 2);
            builder.Property(x => x.EffectiveScore).IsRequired().HasPrecision(5, 2);
            builder.Property(x => x.CalculationVersion).IsRequired();
            builder.Property(x => x.CalculatedAt).IsRequired();
            builder.Property(x => x.OverrideUserId);
            builder.Property(x => x.OverrideAt);
            builder.Property(x => x.OverrideReason).HasMaxLength(2000);

            builder.HasOne(x => x.ErrorGroup).WithOne(x => x.Criticality).HasForeignKey<ErrorCriticality>(x => x.ErrorGroupId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.ErrorGroupId).IsUnique();
            builder.HasIndex(x => x.EffectiveScore);
            builder.HasIndex(x => new
            {
                x.EffectiveScore,
                x.CalculatedAt
            });
        }
    }
}
