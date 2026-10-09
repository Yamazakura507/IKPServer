using IKP.Domain.Common.Audit;
using IKP.Domain.Common.Interfaces.Audit;
using IKP.Domain.Entities.Logs;
using IKP.Infrastructure.Persistence;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using IKP.Infrastructure.Services.Audit.Interfaces;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace IKP.Infrastructure.Interceptors
{
    public class AuditSaveChangesInterceptor : SaveChangesInterceptor
    {
        private static readonly HashSet<string> AuditSystemProperties = new(StringComparer.Ordinal)
        {
            nameof(IAuditable.CreatedAt),
            nameof(IAuditable.CreatedBy),
            nameof(IAuditable.UpdatedAt),
            nameof(IAuditable.UpdatedBy),
            nameof(ISoftDelete.IsDeleted),
            nameof(ISoftDelete.DeletedAt),
            nameof(ISoftDelete.DeletedBy)
        };

        private readonly IAuditValueSerializer valueSerializer;
        private readonly IAuditActionRegistry actionRegistry;

        public AuditSaveChangesInterceptor(IAuditValueSerializer valueSerializer, IAuditActionRegistry actionRegistry)
        {
            this.valueSerializer = valueSerializer;
            this.actionRegistry = actionRegistry;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is AppDbContext context)
            {
                ProcessChanges(context);
                CreateAuditLogs(context);
            }

            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is AppDbContext context)
            {
                ProcessChanges(context);
                await CreateAuditLogsAsync(context, cancellationToken);
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void ProcessChanges(AppDbContext context)
        {
            DateTime now = DateTime.UtcNow;
            IEnumerable<EntityEntry> entries = context.ChangeTracker.Entries().Where(x => x.Entity is IAuditable or ISoftDelete);

            foreach (EntityEntry entry in entries)
            {
                if (entry.Entity is IAuditable auditable)
                {
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedAt = now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        auditable.UpdatedAt = now;
                    }
                }

                if (entry.Entity is ISoftDelete softDelete && entry.State == EntityState.Deleted)
                {
                    softDelete.IsDeleted = true;
                    softDelete.DeletedAt = now;

                    entry.State = EntityState.Modified;
                }
            }
        }

        private void CreateAuditLogs(AppDbContext context)
        {
            DateTime now = DateTime.UtcNow;

            foreach (EntityEntry entry in GetAuditEntries(context))
            {
                if (GetAuditActionCode(entry) is not { } actionCode) continue;

                Guid actionId = actionRegistry.GetId(actionCode);

                context.Set<AuditLog>().Add(CreateAuditLog(entry, actionCode, actionId, now));
            }
        }

        private async Task CreateAuditLogsAsync(AppDbContext context, CancellationToken cancellationToken)
        {
            DateTime now = DateTime.UtcNow;

            foreach (EntityEntry entry in GetAuditEntries(context))
            {
                if (GetAuditActionCode(entry) is not { } actionCode) continue;

                Guid actionId = await actionRegistry.GetIdAsync(actionCode, cancellationToken);

                context.Set<AuditLog>().Add(CreateAuditLog(entry, actionCode, actionId, now));
            }
        }

        private AuditLog CreateAuditLog(EntityEntry entry, string actionCode, Guid actionId, DateTime now)
        {
            AuditLog auditLog = new ()
            {
                Id = Guid.NewGuid(),
                EntitySchema = entry.Metadata.GetSchema() ?? DatabaseSchemas.App,
                EntityType = entry.Metadata.ClrType.Name,
                EntityId = GetEntityId(entry),
                ActionId = actionId,
                UserId = null,
                CreatedAt = now
            };

            if (actionCode == AuditActionCodes.Update) AddChanges(entry, auditLog);

            return auditLog;
        }

        private void AddChanges(EntityEntry entry, AuditLog auditLog)
        {
            foreach (PropertyEntry property in entry.Properties)
            {
                if (!property.IsModified || property.Metadata.IsPrimaryKey() || AuditSystemProperties.Contains(property.Metadata.Name)) continue;

                object? oldValue = property.OriginalValue;
                object? newValue = property.CurrentValue;

                if (Equals(oldValue, newValue)) continue;

                auditLog.Changes.Add(
                    new AuditLogChange
                    {
                        Id = Guid.NewGuid(),
                        PropertyName = property.Metadata.Name,
                        PropertyType = property.Metadata.ClrType.GetPropertyType(),
                        OldValue = valueSerializer.Serialize(oldValue, property.Metadata.ClrType),
                        NewValue = valueSerializer.Serialize(newValue, property.Metadata.ClrType)
                    });
            }
        }

        private static IEnumerable<EntityEntry> GetAuditEntries(AppDbContext context)
        {
            return context.ChangeTracker
                .Entries()
                .Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                .Where(x => x.Entity is not (AuditLog or AuditLogChange))
                .ToArray();
        }

        private static string? GetAuditActionCode(EntityEntry entry) => entry.State switch
        {
            EntityState.Added => AuditActionCodes.Create,
            EntityState.Deleted => AuditActionCodes.Delete,
            EntityState.Modified => GetAuditActionCodesModified(entry),
            _ => null
        };

        private static string GetAuditActionCodesModified(EntityEntry entry)
        {
            if (entry.Entity is ISoftDelete softDelete)
            {
                bool originalIsDeleted = entry.Property(nameof(ISoftDelete.IsDeleted)).OriginalValue is bool value && value;

                if (!originalIsDeleted && softDelete.IsDeleted) return AuditActionCodes.Delete;
                if (originalIsDeleted && !softDelete.IsDeleted) return AuditActionCodes.Restore;
            }

            return AuditActionCodes.Update;
        }

        private static Guid GetEntityId(EntityEntry entry) => (Guid)entry.Properties.First(x => x.Metadata.IsPrimaryKey()).CurrentValue!;
    }
}
