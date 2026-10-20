using IKP.Domain.Entities.Updates;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Updates
{
    public sealed class UpdateDownloadLinkConfiguration : IEntityTypeConfiguration<UpdateDownloadLink>
    {
        public void Configure(EntityTypeBuilder<UpdateDownloadLink> builder)
        {
            builder.ToTable("DownloadLinks", DatabaseSchemas.Updates);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.VersionId).IsRequired();
            builder.Property(x => x.Url).HasMaxLength(4000).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(250);
            builder.Property(x => x.IsPrimary).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();

            builder.HasIndex(x => new { x.VersionId, x.IsPrimary }).IsUnique().HasFilter("\"IsPrimary\" = true");

            builder.HasOne(x => x.Version).WithMany().HasForeignKey(x => x.VersionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
