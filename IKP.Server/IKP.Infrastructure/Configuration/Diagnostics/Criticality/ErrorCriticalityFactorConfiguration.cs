using IKP.Domain.Entities.Diagnostics.Criticality;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Criticality
{
    public class ErrorCriticalityFactorConfiguration : IEntityTypeConfiguration<ErrorCriticalityFactor>
    {
        public void Configure(EntityTypeBuilder<ErrorCriticalityFactor> builder)
        {
            builder.ToTable("ErrorCriticalityFactors", DatabaseSchemas.Diagnostics);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ErrorCriticalityId).IsRequired();
            builder.Property(x => x.DefinitionId).IsRequired();
            builder.Property(x => x.Score).IsRequired().HasPrecision(5, 2);
            builder.Property(x => x.Weight).IsRequired().HasPrecision(5, 4);
            builder.Property(x => x.Contribution).IsRequired().HasPrecision(7, 4);
            builder.Property(x => x.Value).HasPrecision(18, 4);
            builder.Property(x => x.CalculatedAt).IsRequired();

            builder.HasOne(x => x.ErrorCriticality).WithMany(x => x.Factors).HasForeignKey(x => x.ErrorCriticalityId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Definition).WithMany().HasForeignKey(x => x.DefinitionId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.ErrorCriticalityId, x.DefinitionId }).IsUnique();
            builder.HasIndex(x => x.DefinitionId);
        }
    }
}
