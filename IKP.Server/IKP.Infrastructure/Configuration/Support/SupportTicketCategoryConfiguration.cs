using IKP.Domain.Entities.Files;
using IKP.Domain.Entities.Support.Lookups;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IKP.Infrastructure.Configuration.Support;
/// <summary>Configures support ticket category lookup entries.</summary>
public sealed class SupportTicketCategoryConfiguration : IEntityTypeConfiguration<SupportTicketCategory>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<SupportTicketCategory> builder)
    {
        builder.ToTable("TicketCategories", DatabaseSchemas.SupportLookup);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
        builder.Property(x => x.NameKey).IsRequired().HasMaxLength(200);
        builder.Property(x => x.DescriptionKey).HasMaxLength(500);
        builder.Property(x => x.FileId);
        builder.Property(x => x.SortOrder).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsSystem).IsRequired();

        builder.HasOne<StoredFile>().WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.FileId);
        builder.HasIndex(x => x.Code).IsUnique();
        builder.HasIndex(x => new { x.IsActive, x.SortOrder });
    }
}
