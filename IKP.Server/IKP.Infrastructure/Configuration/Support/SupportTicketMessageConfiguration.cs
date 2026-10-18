using IKP.Domain.Entities.Support;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IKP.Infrastructure.Configuration.Support;
/// <summary>Configures support ticket messages.</summary>
public sealed class SupportTicketMessageConfiguration : IEntityTypeConfiguration<SupportTicketMessage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<SupportTicketMessage> builder)
    {
        builder.ToTable("TicketMessages", DatabaseSchemas.Support);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TicketId).IsRequired();
        builder.Property(x => x.AuthorUserId).IsRequired();
        builder.Property(x => x.IsInternal).IsRequired();
        builder.Property(x => x.Content).IsRequired().HasMaxLength(20000);
        builder.Property(x => x.SentAt).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasIndex(x => new { x.TicketId, x.SentAt });
        builder.HasIndex(x => new { x.TicketId, x.IsInternal, x.SentAt });

        builder.HasOne(x => x.Ticket).WithMany(x => x.Messages).HasForeignKey(x => x.TicketId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.AuthorUser).WithMany().HasForeignKey(x => x.AuthorUserId).OnDelete(DeleteBehavior.Restrict);
    }
}
