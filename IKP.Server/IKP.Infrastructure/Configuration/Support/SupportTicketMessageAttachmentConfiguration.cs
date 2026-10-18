using IKP.Domain.Entities.Support;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IKP.Infrastructure.Configuration.Support;
/// <summary>Configures support message attachments.</summary>
public sealed class SupportTicketMessageAttachmentConfiguration : IEntityTypeConfiguration<SupportTicketMessageAttachment>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<SupportTicketMessageAttachment> builder)
    {
        builder.ToTable("TicketMessageAttachments", DatabaseSchemas.Support);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.MessageId).IsRequired();
        builder.Property(x => x.FileId).IsRequired();

        builder.HasIndex(x => new { x.MessageId, x.FileId }).IsUnique();
        builder.HasIndex(x => x.FileId);

        builder.HasOne(x => x.Message).WithMany(x => x.Attachments).HasForeignKey(x => x.MessageId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.File).WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.Restrict);
    }
}
