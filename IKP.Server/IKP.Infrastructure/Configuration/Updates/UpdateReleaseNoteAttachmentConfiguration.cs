using IKP.Domain.Entities.Updates;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Updates
{
    public sealed class UpdateReleaseNoteAttachmentConfiguration : IEntityTypeConfiguration<UpdateReleaseNoteAttachment>
    {
        public void Configure(EntityTypeBuilder<UpdateReleaseNoteAttachment> builder)
        {
            builder.ToTable("ReleaseNoteAttachments", DatabaseSchemas.Updates);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.ReleaseNoteId).IsRequired();
            builder.Property(x => x.FileId).IsRequired();

            builder.HasIndex(x => new { x.ReleaseNoteId, x.SortOrder });
            builder.HasIndex(x => x.FileId);

            builder.HasOne(x => x.ReleaseNote).WithMany(x => x.Attachments).HasForeignKey(x => x.ReleaseNoteId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.File).WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
