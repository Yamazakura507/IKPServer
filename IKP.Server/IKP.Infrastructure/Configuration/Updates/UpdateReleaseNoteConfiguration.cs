using IKP.Domain.Entities.Updates;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Updates
{
    public sealed class UpdateReleaseNoteConfiguration : IEntityTypeConfiguration<UpdateReleaseNote>
    {
        public void Configure(EntityTypeBuilder<UpdateReleaseNote> builder)
        {
            builder.ToTable("ReleaseNotes", DatabaseSchemas.Updates);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.VersionId).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(12000);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.IsPlanned).IsRequired();
            builder.Property(x => x.IsHtmlDescription).IsRequired();

            builder.HasIndex(x => new { x.VersionId, x.SortOrder });

            builder.HasOne(x => x.Version).WithMany().HasForeignKey(x => x.VersionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
