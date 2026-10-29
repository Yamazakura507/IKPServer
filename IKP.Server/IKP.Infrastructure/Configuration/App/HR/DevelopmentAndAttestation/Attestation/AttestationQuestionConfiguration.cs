using IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Attestation;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.DevelopmentAndAttestation.Attestation
{
    public sealed class AttestationQuestionConfiguration : IEntityTypeConfiguration<AttestationQuestion>
    {
        public void Configure(EntityTypeBuilder<AttestationQuestion> builder)
        {
            builder.ToTable("AttestationQuestions", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SectionId).IsRequired();
            builder.Property(x => x.QuestionTypeId).IsRequired();
            builder.Property(x => x.Text).IsRequired().HasMaxLength(8000);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.IsAiDraft).IsRequired();
            builder.Property(x => x.IsApproved).IsRequired();
            builder.Property(x => x.IsOralAnswer).IsRequired();
            builder.Property(x => x.IsSelfWriteAnswer).IsRequired();
            builder.Property(x => x.AiPromptContext);
            builder.Property(x => x.MaxScore).HasPrecision(6, 2);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasIndex(x => new { x.SectionId, x.SortOrder });

            builder.HasOne(x => x.Section).WithMany(x => x.Questions).HasForeignKey(x => x.SectionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.QuestionType).WithMany().HasForeignKey(x => x.QuestionTypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
