using IKP.Domain.Entities.Files;
using IKP.Domain.Entities.Auth.Lookup;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Auth.Lookup;

/// <summary>Configures authentication method lookup storage.</summary>
public class AuthenticationMethodDefinitionConfiguration : IEntityTypeConfiguration<AuthenticationMethodDefinition>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AuthenticationMethodDefinition> builder)
    {
        builder.ToTable("AuthenticationMethods", DatabaseSchemas.AuthLookup);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
        builder.Property(x => x.NameKey).IsRequired().HasMaxLength(200);
        builder.Property(x => x.DescriptionKey).HasMaxLength(500);
        builder.Property(x => x.FileId);
        builder.HasOne<StoredFile>()
            .WithMany()
            .HasForeignKey(x => x.FileId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.FileId);
        builder.Property(x => x.SortOrder).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsSystem).IsRequired();

        builder.HasIndex(x => x.Code).IsUnique();
        builder.HasIndex(x => new { x.IsActive, x.SortOrder });
    }
}
