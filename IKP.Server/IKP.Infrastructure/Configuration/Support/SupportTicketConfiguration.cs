using IKP.Domain.Entities.Support;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IKP.Infrastructure.Configuration.Support;
/// <summary>Configures support tickets.</summary>
public sealed class SupportTicketConfiguration : IEntityTypeConfiguration<SupportTicket>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<SupportTicket> builder)
    {
        builder.ToTable("Tickets", DatabaseSchemas.Support);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Number).ValueGeneratedOnAdd();
        builder.Property(x => x.Subject).IsRequired().HasMaxLength(250);
        builder.Property(x => x.CreatedByUserId).IsRequired();
        builder.Property(x => x.CategoryId).IsRequired();
        builder.Property(x => x.PriorityId).IsRequired();
        builder.Property(x => x.StatusId).IsRequired();
        builder.Property(x => x.AssignedToUserId);
        builder.Property(x => x.LastMessageAt).IsRequired();
        builder.Property(x => x.ResolvedAt);
        builder.Property(x => x.ResolvedByUserId);
        builder.Property(x => x.ClosedAt);
        builder.Property(x => x.ClosedByUserId);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasIndex(x => x.Number).IsUnique();
        builder.HasIndex(x => new { x.StatusId, x.PriorityId, x.LastMessageAt });
        builder.HasIndex(x => new { x.CreatedByUserId, x.CreatedAt });
        builder.HasIndex(x => new { x.AssignedToUserId, x.StatusId });

        builder.HasOne(x => x.CreatedByUser).WithMany().HasForeignKey(x => x.CreatedByUserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.AssignedToUser).WithMany().HasForeignKey(x => x.AssignedToUserId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Priority).WithMany().HasForeignKey(x => x.PriorityId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).OnDelete(DeleteBehavior.Restrict);
    }
}
