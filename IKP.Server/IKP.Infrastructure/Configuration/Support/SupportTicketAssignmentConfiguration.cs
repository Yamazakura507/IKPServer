using IKP.Domain.Entities.Support;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IKP.Infrastructure.Configuration.Support;
/// <summary>Configures support ticket assignment history.</summary>
public sealed class SupportTicketAssignmentConfiguration : IEntityTypeConfiguration<SupportTicketAssignment>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<SupportTicketAssignment> builder)
    {
        builder.ToTable("TicketAssignments", DatabaseSchemas.Support);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TicketId).IsRequired();
        builder.Property(x => x.AssignedToUserId);
        builder.Property(x => x.AssignedByUserId).IsRequired();
        builder.Property(x => x.AssignedAt).IsRequired();
        builder.Property(x => x.UnassignedAt);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);

        builder.HasIndex(x => new { x.TicketId, x.AssignedAt });
        builder.HasIndex(x => x.TicketId).HasFilter("\"UnassignedAt\" IS NULL").IsUnique();
        builder.HasIndex(x => new { x.AssignedToUserId, x.UnassignedAt });

        builder.HasOne(x => x.Ticket).WithMany(x => x.Assignments).HasForeignKey(x => x.TicketId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.AssignedToUser).WithMany().HasForeignKey(x => x.AssignedToUserId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.AssignedByUser).WithMany().HasForeignKey(x => x.AssignedByUserId).OnDelete(DeleteBehavior.Restrict);
    }
}
