using IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Attestation;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.DevelopmentAndAttestation.Attestation
{
    public sealed class AttestationAnswerOptionConfiguration : IEntityTypeConfiguration<AttestationAnswerOption>
    {
        public void Configure(EntityTypeBuilder<AttestationAnswerOption> builder)
        {
            builder.ToTable("AttestationAnswerOptions", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.QuestionId).IsRequired();
            builder.Property(x => x.IsCorrect).IsRequired();
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.Text).IsRequired().HasMaxLength(4000);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasIndex(x => new { x.QuestionId, x.SortOrder });

            builder.HasOne(x => x.Question).WithMany(x => x.Options).HasForeignKey(x => x.QuestionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
