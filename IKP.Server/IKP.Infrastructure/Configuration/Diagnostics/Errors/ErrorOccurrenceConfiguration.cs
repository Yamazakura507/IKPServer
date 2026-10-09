using IKP.Domain.Entities.Diagnostics.Errors;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Errors
{
    public class ErrorOccurrenceConfiguration : IEntityTypeConfiguration<ErrorOccurrence>
    {
        public void Configure(EntityTypeBuilder<ErrorOccurrence> builder)
        {
            builder.ToTable("ErrorOccurrences", DatabaseSchemas.Diagnostics);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ErrorGroupId).IsRequired();
            builder.Property(x => x.OccurredAt).IsRequired();
            builder.Property(x => x.Context).HasColumnName("ContextSnapshot").HasColumnType("jsonb");

            builder.HasOne(x => x.ErrorGroup).WithMany(x => x.Occurrences).HasForeignKey(x => x.ErrorGroupId).OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(
                x => x.Actor,
                actor =>
                {
                    actor.Property(x => x.UserId).HasColumnName("UserId");
                    actor.Property(x => x.SessionId).HasColumnName("SessionId").HasMaxLength(200);

                    actor.HasIndex(x => x.UserId);
                });
            builder.OwnsOne(
                x => x.Application,
                application =>
                {
                    application.Property(x => x.ApplicationId).HasColumnName("ApplicationId").HasMaxLength(100);
                    application.Property(x => x.ApplicationVersion).HasColumnName("ApplicationVersion").HasMaxLength(100);
                    application.Property(x => x.Platform).HasColumnName("Platform").HasMaxLength(100);
                    application.Property(x => x.MachineId).HasColumnName("MachineId").HasMaxLength(200);
                    application.Property(x => x.ClientInstanceId).HasColumnName("ClientInstanceId").HasMaxLength(200);

                    application.HasIndex(x => x.ApplicationVersion);
                    application.HasIndex(x => x.Platform);
                });
            builder.OwnsOne(
                x => x.Request,
                request =>
                {
                    request.Property(x => x.RequestId).HasColumnName("RequestId").HasMaxLength(200);
                    request.Property(x => x.CorrelationId).HasColumnName("CorrelationId").HasMaxLength(200);
                    request.Property(x => x.Endpoint).HasColumnName("Endpoint").HasMaxLength(1000);
                    request.Property(x => x.HttpMethod).HasColumnName("HttpMethod").HasMaxLength(20);
                    request.Property(x => x.Route).HasColumnName("Route").HasMaxLength(1000);

                    request.HasIndex(x => x.RequestId);
                    request.HasIndex(x => x.CorrelationId);
                });
            builder.OwnsOne(
                x => x.Exception,
                exception =>
                {
                    exception.Property(x => x.ExceptionType).HasColumnName("ExceptionType").HasMaxLength(500);
                    exception.Property(x => x.Message).HasColumnName("ExceptionMessage");
                    exception.Property(x => x.StackTrace).HasColumnName("StackTrace");
                    exception.Property(x => x.SourceFile).HasColumnName("SourceFile").HasMaxLength(1000);
                    exception.Property(x => x.SourceMethod).HasColumnName("SourceMethod").HasMaxLength(1000);
                    exception.Property(x => x.SourceLineNumber).HasColumnName("SourceLineNumber");
                });

            builder.HasIndex(x => new
            {
                x.ErrorGroupId,
                x.OccurredAt
            });
            builder.HasIndex(x => x.OccurredAt);
        }
    }
}
