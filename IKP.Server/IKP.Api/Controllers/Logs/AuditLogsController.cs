using IKP.Application.Auth.Authorization;
using Microsoft.AspNetCore.Authorization;
using IKP.Api.Diagnostics.Models.Logs;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Common.Query;
using IKP.Domain.Common.Interfaces.Audit;
using IKP.Domain.Entities.Logs;
using IKP.Infrastructure.Persistence;
using IKP.Infrastructure.Services.Audit.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using IKP.Domain.Common.Codes.Audit;
using IKP.Infrastructure.Persistence.Seeds.Seeds;

namespace IKP.Api.Controllers.Logs;

/// <summary>
/// Provides read and controlled rollback operations for audit logs.
/// </summary>
[ApiController]
[Authorize(Policy = AuthorizationPolicyNames.AuditRead)]
[Route("api/logs/audit")]
public sealed class AuditLogsController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<AuditLog, object?>>> Fields =
        new Dictionary<string, Expression<Func<AuditLog, object?>>>(StringComparer.OrdinalIgnoreCase)
        {
            ["id"] = x => x.Id,
            ["entitySchema"] = x => x.EntitySchema,
            ["entityType"] = x => x.EntityType,
            ["entityId"] = x => x.EntityId,
            ["actionId"] = x => x.ActionId,
            ["actionCode"] = x => x.Action.Code,
            ["userId"] = x => x.UserId,
            ["restorLogId"] = x => x.RestorLogId,
            ["createdAt"] = x => x.CreatedAt
        };

    private readonly IDbContextFactory<AppDbContext> dbContextFactory;
    private readonly IAuditValueSerializer valueSerializer;

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    public AuditLogsController(IDbContextFactory<AppDbContext> dbContextFactory, IAuditValueSerializer valueSerializer)
    {
        this.dbContextFactory = dbContextFactory;
        this.valueSerializer = valueSerializer;
    }

    /// <summary>
    /// Queries audit logs with recursive filters, multi-column sorting, search and pagination.
    /// </summary>
    [HttpPost("query")]
    public async Task<ActionResult<PagedResponse<AuditLogResponse>>> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        PagedResponse<AuditLogResponse> response = await db.AuditLogs.AsNoTracking().ToPagedResponseAsync(
            request,
            Fields,
            x => new AuditLogResponse
            {
                Id = x.Id,
                EntitySchema = x.EntitySchema,
                EntityType = x.EntityType,
                EntityId = x.EntityId,
                ActionId = x.ActionId,
                ActionCode = x.Action.Code,
                UserId = x.UserId,
                RestorLogId = x.RestorLogId,
                CreatedAt = x.CreatedAt
            },
            cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Gets one audit log including property changes.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AuditLogResponse>> Get(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        AuditLogResponse? response = await BuildResponseQuery(db, id).SingleOrDefaultAsync(cancellationToken);

        return response is null ? NotFound() : Ok(response);
    }

    /// <summary>
    /// Rolls an audit event back according to its action semantics.
    /// </summary>
    [HttpPost("{id:guid}/rollback")]
    [Authorize(Policy = AuthorizationPolicyNames.AuditRollback)]
    public async Task<ActionResult<AuditLogResponse>> Rollback(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        await using IDbContextTransaction transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        AuditLog? auditLog = await db.AuditLogs.Include(x => x.Action).Include(x => x.Changes).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (auditLog is null) return NotFound();

        if (auditLog.Action.Code is AuditActionCodes.Update or AuditActionCodes.Delete && await HasRestoreLogAsync(db, auditLog.Id, cancellationToken))
        {
            return Conflict("This audit event already has a corresponding restore event and cannot be rolled back again.");
        }

        if (auditLog.Action.Code is AuditActionCodes.RestoreUpdate or AuditActionCodes.RestoreDelete)
        {
            return Ok(await RollbackRestoreAsync(db, auditLog, cancellationToken));
        }

        bool hasLaterAudit = await db.AuditLogs.AnyAsync(
            x => x.EntitySchema == auditLog.EntitySchema
                && x.EntityType == auditLog.EntityType
                && x.EntityId == auditLog.EntityId
                && x.CreatedAt > auditLog.CreatedAt,
            cancellationToken);

        if (hasLaterAudit) return Conflict("The selected audit event is not the latest event for this entity.");

        IEntityType? entityType = db.Model.GetEntityTypes().FirstOrDefault(x =>
            string.Equals(x.GetSchema(), auditLog.EntitySchema, StringComparison.OrdinalIgnoreCase)
            && string.Equals(x.ClrType.Name, auditLog.EntityType, StringComparison.Ordinal));

        if (entityType is null) return Conflict($"Audited entity type '{auditLog.EntitySchema}.{auditLog.EntityType}' is not available in the current model.");

        object? entity = await db.FindAsync(entityType.ClrType, [auditLog.EntityId], cancellationToken);
        bool auditLogDeleted = false;

        switch (auditLog.Action.Code)
        {
            case AuditActionCodes.Update:
                if (entity is null) return Conflict("The audited entity no longer exists.");

                RollbackUpdate(db, entity, auditLog);
                db.AuditLogs.Add(CreateRestoreLog(auditLog, AuditActionCodes.RestoreUpdate));
                break;
            case AuditActionCodes.Delete:
                if (entity is not ISoftDelete softDelete) return Conflict("The deleted entity does not support restoration.");

                softDelete.IsDeleted = false;
                softDelete.DeletedAt = null;
                softDelete.DeletedBy = null;
                db.AuditLogs.Add(CreateRestoreLog(auditLog, AuditActionCodes.RestoreDelete));
                break;
            case AuditActionCodes.Create:
                if (entity is not null) db.Remove(entity);

                db.AuditLogs.Remove(auditLog);
                auditLogDeleted = true;
                break;
            default:
                return Conflict($"Audit action '{auditLog.Action.Code}' cannot be rolled back.");
        }

        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return auditLogDeleted ? NoContent() : Ok(await BuildResponseQuery(db, id).SingleOrDefaultAsync(cancellationToken));
    }

    private async Task<AuditLogResponse> RollbackRestoreAsync(AppDbContext db, AuditLog restoreLog, CancellationToken cancellationToken)
    {
        if (restoreLog.RestorLogId is null) throw new InvalidOperationException("The restore audit log does not reference the audit event it restored.");
        
        AuditLog? originalLog = await db.AuditLogs.Include(x => x.Changes).Include(x => x.Action).SingleOrDefaultAsync(x => x.Id == restoreLog.RestorLogId, cancellationToken);
        
        if (originalLog is null) throw new InvalidOperationException("The original audit event referenced by the restore log no longer exists.");

        IEntityType? entityType = db.Model.GetEntityTypes().FirstOrDefault(x =>
            string.Equals(x.GetSchema(), originalLog.EntitySchema, StringComparison.OrdinalIgnoreCase)
            && string.Equals(x.ClrType.Name, originalLog.EntityType, StringComparison.Ordinal));
        
        if (entityType is null) throw new InvalidOperationException($"Audited entity type '{originalLog.EntitySchema}.{originalLog.EntityType}' is not available in the current model.");
       
        object? entity = await db.FindAsync(entityType.ClrType, [originalLog.EntityId], cancellationToken);

        switch (originalLog.Action.Code)
        {
            case AuditActionCodes.Update:
                if (entity is null) throw new InvalidOperationException("The entity referenced by the original update audit event no longer exists.");

                ReapplyUpdate(db, entity, originalLog);
                break;
            case AuditActionCodes.Delete:
                if (entity is not ISoftDelete softDelete) throw new InvalidOperationException("The entity referenced by the original delete audit event does not support soft deletion.");
                
                softDelete.IsDeleted = true;
                softDelete.DeletedAt = DateTime.UtcNow;
                break;
            default:
                throw new InvalidOperationException($"Restore event references unsupported action '{originalLog.Action.Code}'.");
        }

        originalLog.CreatedAt = DateTime.UtcNow;

        db.AuditLogs.Remove(restoreLog);
        await db.SaveChangesAsync(cancellationToken);
        await db.Database.CommitTransactionAsync(cancellationToken);

        return await BuildResponseQuery(db, originalLog.Id).SingleAsync(cancellationToken);
    }

    private async Task<bool> HasRestoreLogAsync(AppDbContext db, Guid auditLogId, CancellationToken cancellationToken) => await db.AuditLogs.AnyAsync(x => x.RestorLogId == auditLogId, cancellationToken);

    private void RollbackUpdate(AppDbContext db, object entity, AuditLog auditLog)
    {
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry = db.Entry(entity);

        foreach (AuditLogChange change in auditLog.Changes)
        {
            IProperty? property = entry.Metadata.FindProperty(change.PropertyName);

            if (property is null || property.IsPrimaryKey()) continue;

            object? expectedCurrentValue = valueSerializer.Deserialize(change.NewValue, property.ClrType);

            if (!Equals(entry.Property(property.Name).CurrentValue, expectedCurrentValue)) throw new InvalidOperationException($"The entity property '{change.PropertyName}' has changed since the selected audit event. Rollback was refused.");
            
            entry.Property(property.Name).CurrentValue = valueSerializer.Deserialize(change.OldValue, property.ClrType);
        }
    }

    private void ReapplyUpdate(AppDbContext db, object entity, AuditLog auditLog)
    {
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry = db.Entry(entity);

        foreach (AuditLogChange change in auditLog.Changes)
        {
            IProperty? property = entry.Metadata.FindProperty(change.PropertyName);
            
            if (property is null || property.IsPrimaryKey()) continue;
            
            object? expectedCurrentValue = valueSerializer.Deserialize(change.OldValue, property.ClrType);
            
            if (!Equals(entry.Property(property.Name).CurrentValue, expectedCurrentValue)) throw new InvalidOperationException($"The entity property '{change.PropertyName}' has changed since the restore event. Restore cancellation was refused.");
            
            entry.Property(property.Name).CurrentValue = valueSerializer.Deserialize(change.NewValue, property.ClrType);
        }
    }

    private static AuditLog CreateRestoreLog(AuditLog source, string actionCode) => new AuditLog
    {
        Id = Guid.NewGuid(),
        EntitySchema = source.EntitySchema,
        EntityType = source.EntityType,
        EntityId = source.EntityId,
        ActionId = actionCode == AuditActionCodes.RestoreUpdate ? AuditActionSeed.RestoreUpdateId : AuditActionSeed.RestoreDeleteId,
        RestorLogId = source.Id,
        CreatedAt = DateTime.UtcNow
    };

    private static IQueryable<AuditLogResponse> BuildResponseQuery(AppDbContext db, Guid id) => db.AuditLogs.AsNoTracking()
    .Where(x => x.Id == id)
    .Select(x => new AuditLogResponse
        {
            Id = x.Id,
            EntitySchema = x.EntitySchema,
            EntityType = x.EntityType,
            EntityId = x.EntityId,
            ActionId = x.ActionId,
            ActionCode = x.Action.Code,
            UserId = x.UserId,
            RestorLogId = x.RestorLogId,
            CreatedAt = x.CreatedAt,
            Changes = x.Changes.OrderBy(y => y.PropertyName).Select(y => new AuditLogChangeResponse
            {
                Id = y.Id,
                PropertyName = y.PropertyName,
                PropertyType = y.PropertyType,
                OldValue = y.OldValue,
                NewValue = y.NewValue
            }).ToList()
        });
}
