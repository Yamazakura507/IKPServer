using IKP.Application.Support.Exceptions;
using IKP.Application.Support.Interfaces;
using IKP.Application.Support.Models;
using IKP.Domain.Common.Support;
using IKP.Domain.Entities.Support;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.Support;

/// <summary>
/// Implements support ticket lifecycle and conversation operations.
/// </summary>
public sealed class SupportService : ISupportService
{
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    /// <summary>
    /// Initializes the support service.
    /// </summary>
    public SupportService(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    /// <inheritdoc />
    public async ValueTask<SupportTicketResponse> CreateTicketAsync(CreateSupportTicketRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        ValidateSubject(request.Subject);
        ValidateMessage(request.Message);

        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        await ValidateUserAsync(db, actorUserId, cancellationToken);
        await ValidateLookupsAsync(db, request.CategoryId, request.PriorityId, null, cancellationToken);

        IReadOnlyList<Guid> attachmentIds = NormalizeAttachmentIds(request.AttachmentFileIds);

        await ValidateAttachmentsAsync(db, attachmentIds, cancellationToken);

        DateTime now = DateTime.UtcNow;

        SupportTicket ticket = new()
        {
            Subject = request.Subject.Trim(),
            CreatedByUserId = actorUserId,
            CategoryId = request.CategoryId,
            PriorityId = request.PriorityId,
            StatusId = await db.SupportTicketStatuses.Where(x => x.Code == SupportTicketStatusCodes.New).Select(x => x.Id).SingleAsync(cancellationToken),
            LastMessageAt = now,
            CreatedBy = actorUserId
        };
        SupportTicketMessage message = new()
        {
            Ticket = ticket,
            AuthorUserId = actorUserId,
            Content = request.Message.Trim(),
            SentAt = now,
            CreatedBy = actorUserId
        };

        foreach (Guid fileId in attachmentIds)
        {
            message.Attachments.Add(new SupportTicketMessageAttachment { FileId = fileId });
        }

        db.SupportTickets.Add(ticket);
        db.SupportTicketMessages.Add(message);
        await db.SaveChangesAsync(cancellationToken);

        return await BuildTicketResponseAsync(db, ticket.Id, cancellationToken) ?? throw new SupportOperationException(500, "SUPPORT_TICKET_CREATE_FAILED", "The support ticket could not be created.");
    }

    /// <inheritdoc />
    public async ValueTask<object?> GetTicketAsync(Guid ticketId, bool includeInternalNotes, CancellationToken cancellationToken = default)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        SupportTicket? ticket = await db.SupportTickets.AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Priority)
            .Include(x => x.Status)
            .FirstOrDefaultAsync(x => x.Id == ticketId && !x.IsDeleted, cancellationToken);

        if (ticket is null) return null;

        List<SupportTicketMessageResponse> messages = await db.SupportTicketMessages.AsNoTracking()
            .Where(x => x.TicketId == ticketId && !x.IsDeleted && (includeInternalNotes || !x.IsInternal))
            .OrderBy(x => x.SentAt)
            .Select(x => new SupportTicketMessageResponse
            {
                Id = x.Id,
                TicketId = x.TicketId,
                AuthorUserId = x.AuthorUserId,
                IsInternal = x.IsInternal,
                Content = x.Content,
                SentAt = x.SentAt,
                AttachmentFileIds = x.Attachments.Select(a => a.FileId).ToList()
            })
            .ToListAsync(cancellationToken);

        return new
        {
            ticket.Id,
            ticket.Number,
            ticket.Subject,
            ticket.CreatedByUserId,
            ticket.CategoryId,
            CategoryCode = ticket.Category.Code,
            ticket.PriorityId,
            PriorityCode = ticket.Priority.Code,
            ticket.StatusId,
            StatusCode = ticket.Status.Code,
            ticket.AssignedToUserId,
            ticket.LastMessageAt,
            ticket.ResolvedAt,
            ticket.ResolvedByUserId,
            ticket.ClosedAt,
            ticket.ClosedByUserId,
            ticket.CreatedAt,
            ticket.UpdatedAt,
            Messages = messages
        };
    }

    /// <inheritdoc />
    public async ValueTask<SupportTicketResponse> UpdateTicketAsync(Guid ticketId, UpdateSupportTicketRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        ValidateSubject(request.Subject);

        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        SupportTicket ticket = await GetTicketEntityAsync(db, ticketId, cancellationToken);

        await ValidateUserAsync(db, actorUserId, cancellationToken);
        await ValidateLookupsAsync(db, request.CategoryId, request.PriorityId, null, cancellationToken);

        ticket.Subject = request.Subject.Trim();
        ticket.CategoryId = request.CategoryId;
        ticket.PriorityId = request.PriorityId;
        ticket.UpdatedBy = actorUserId;

        await db.SaveChangesAsync(cancellationToken);

        return await BuildTicketResponseAsync(db, ticket.Id, cancellationToken) ?? throw new SupportOperationException(500, "SUPPORT_TICKET_UPDATE_FAILED", "The support ticket could not be read after update.");
    }

    /// <inheritdoc />
    public async ValueTask<SupportTicketResponse> SetStatusAsync(Guid ticketId, SetSupportTicketStatusRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        SupportTicket ticket = await GetTicketEntityAsync(db, ticketId, cancellationToken);

        await ValidateUserAsync(db, actorUserId, cancellationToken);
        await ValidateLookupsAsync(db, null, null, request.StatusId, cancellationToken);

        string targetCode = await db.SupportTicketStatuses.Where(x => x.Id == request.StatusId).Select(x => x.Code).SingleAsync(cancellationToken);
        DateTime now = DateTime.UtcNow;

        ticket.StatusId = request.StatusId;
        ticket.UpdatedBy = actorUserId;

        if (targetCode == SupportTicketStatusCodes.Resolved)
        {
            ticket.ResolvedAt ??= now;
            ticket.ResolvedByUserId = actorUserId;
            ticket.ClosedAt = null;
            ticket.ClosedByUserId = null;
        }
        else if (targetCode == SupportTicketStatusCodes.Closed)
        {
            ticket.ClosedAt ??= now;
            ticket.ClosedByUserId = actorUserId;
        }
        else
        {
            ticket.ResolvedAt = null;
            ticket.ResolvedByUserId = null;
            ticket.ClosedAt = null;
            ticket.ClosedByUserId = null;
        }

        await db.SaveChangesAsync(cancellationToken);

        return await BuildTicketResponseAsync(db, ticket.Id, cancellationToken) ?? throw new SupportOperationException(500, "SUPPORT_STATUS_UPDATE_FAILED", "The support ticket could not be read after status update.");
    }

    /// <inheritdoc />
    public async ValueTask<SupportTicketResponse> AssignAsync(Guid ticketId, AssignSupportTicketRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        SupportTicket ticket = await GetTicketEntityAsync(db, ticketId, cancellationToken);

        await ValidateUserAsync(db, actorUserId, cancellationToken);

        if (request.AssignedToUserId.HasValue) await ValidateUserAsync(db, request.AssignedToUserId.Value, cancellationToken);

        DateTime now = DateTime.UtcNow;

        if (ticket.AssignedToUserId != request.AssignedToUserId)
        {
            SupportTicketAssignment? current = await db.SupportTicketAssignments.FirstOrDefaultAsync(x => x.TicketId == ticket.Id && x.UnassignedAt == null, cancellationToken);
            
            if (current is not null) current.UnassignedAt = now;

            if (request.AssignedToUserId.HasValue)
            {
                db.SupportTicketAssignments.Add(new SupportTicketAssignment
                {
                    TicketId = ticket.Id,
                    AssignedToUserId = request.AssignedToUserId,
                    AssignedByUserId = actorUserId,
                    AssignedAt = now,
                    CreatedBy = actorUserId
                });
            }

            ticket.AssignedToUserId = request.AssignedToUserId;
            ticket.UpdatedBy = actorUserId;
        }

        await db.SaveChangesAsync(cancellationToken);

        return await BuildTicketResponseAsync(db, ticket.Id, cancellationToken) ?? throw new SupportOperationException(500, "SUPPORT_ASSIGNMENT_FAILED", "The support ticket could not be read after assignment.");
    }

    /// <inheritdoc />
    public ValueTask<SupportTicketMessageResponse> AddMessageAsync(Guid ticketId, AddSupportTicketMessageRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
        => AddMessageInternalAsync(ticketId, request.Message, request.AttachmentFileIds, false, actorUserId, cancellationToken);

    /// <inheritdoc />
    public ValueTask<SupportTicketMessageResponse> AddInternalNoteAsync(Guid ticketId, AddSupportInternalNoteRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
        => AddMessageInternalAsync(ticketId, request.Message, request.AttachmentFileIds, true, actorUserId, cancellationToken);

    private async ValueTask<SupportTicketMessageResponse> AddMessageInternalAsync(Guid ticketId, string content, IReadOnlyCollection<Guid>? attachmentFileIds, bool isInternal, Guid actorUserId, CancellationToken cancellationToken)
    {
        ValidateMessage(content);

        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        SupportTicket ticket = await GetTicketEntityAsync(db, ticketId, cancellationToken);

        await ValidateUserAsync(db, actorUserId, cancellationToken);

        IReadOnlyList<Guid> attachmentIds = NormalizeAttachmentIds(attachmentFileIds);

        await ValidateAttachmentsAsync(db, attachmentIds, cancellationToken);

        DateTime now = DateTime.UtcNow;

        SupportTicketMessage message = new()
        {
            TicketId = ticket.Id,
            AuthorUserId = actorUserId,
            IsInternal = isInternal,
            Content = content.Trim(),
            SentAt = now,
            CreatedBy = actorUserId
        };

        foreach (Guid fileId in attachmentIds) message.Attachments.Add(new SupportTicketMessageAttachment { FileId = fileId });

        ticket.LastMessageAt = now;
        ticket.UpdatedBy = actorUserId;

        db.SupportTicketMessages.Add(message);
        await db.SaveChangesAsync(cancellationToken);

        return new SupportTicketMessageResponse
        {
            Id = message.Id,
            TicketId = ticket.Id,
            AuthorUserId = message.AuthorUserId,
            IsInternal = message.IsInternal,
            Content = message.Content,
            SentAt = message.SentAt,
            AttachmentFileIds = attachmentIds
        };
    }

    /// <inheritdoc />
    public async ValueTask DeleteAsync(Guid ticketId, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        SupportTicket ticket = await GetTicketEntityAsync(db, ticketId, cancellationToken);

        await ValidateUserAsync(db, actorUserId, cancellationToken);

        ticket.IsDeleted = true;
        ticket.DeletedBy = actorUserId;

        await db.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask RestoreAsync(Guid ticketId, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        SupportTicket? ticket = await db.SupportTickets.FirstOrDefaultAsync(x => x.Id == ticketId && x.IsDeleted, cancellationToken);

        if (ticket is null) throw new SupportOperationException(404, "SUPPORT_TICKET_NOT_FOUND", "The deleted support ticket was not found.");

        await ValidateUserAsync(db, actorUserId, cancellationToken);

        ticket.IsDeleted = false;
        ticket.DeletedAt = null;
        ticket.DeletedBy = null;
        ticket.UpdatedBy = actorUserId;

        await db.SaveChangesAsync(cancellationToken);
    }

    private static async ValueTask<SupportTicket> GetTicketEntityAsync(AppDbContext db, Guid ticketId, CancellationToken cancellationToken)
        => await db.SupportTickets.FirstOrDefaultAsync(x => x.Id == ticketId && !x.IsDeleted, cancellationToken)
            ?? throw new SupportOperationException(404, "SUPPORT_TICKET_NOT_FOUND", "The support ticket was not found.");

    private static async ValueTask<SupportTicketResponse?> BuildTicketResponseAsync(AppDbContext db, Guid ticketId, CancellationToken cancellationToken)
        => await db.SupportTickets.AsNoTracking().Where(x => x.Id == ticketId && !x.IsDeleted).Select(x => new SupportTicketResponse
        {
            Id = x.Id,
            Number = x.Number,
            Subject = x.Subject,
            CreatedByUserId = x.CreatedByUserId,
            CategoryId = x.CategoryId,
            CategoryCode = x.Category.Code,
            PriorityId = x.PriorityId,
            PriorityCode = x.Priority.Code,
            StatusId = x.StatusId,
            StatusCode = x.Status.Code,
            AssignedToUserId = x.AssignedToUserId,
            LastMessageAt = x.LastMessageAt,
            ResolvedAt = x.ResolvedAt,
            ClosedAt = x.ClosedAt,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        }).SingleOrDefaultAsync(cancellationToken);

    private static async ValueTask ValidateUserAsync(AppDbContext db, Guid userId, CancellationToken cancellationToken)
    {
        bool exists = await db.AuthUsers.AnyAsync(x => x.Id == userId && x.IsActive && !x.IsDeleted, cancellationToken);

        if (!exists) throw new SupportOperationException(400, "SUPPORT_USER_INVALID", "The selected user is not active.");
    }

    private static async ValueTask ValidateLookupsAsync(AppDbContext db, Guid? categoryId, Guid? priorityId, Guid? statusId, CancellationToken cancellationToken)
    {
        if (categoryId.HasValue && !await db.SupportTicketCategories.AnyAsync(x => x.Id == categoryId && x.IsActive, cancellationToken))
            throw new SupportOperationException(400, "SUPPORT_CATEGORY_INVALID", "The selected support category is not active.");
        if (priorityId.HasValue && !await db.SupportTicketPriorities.AnyAsync(x => x.Id == priorityId && x.IsActive, cancellationToken))
            throw new SupportOperationException(400, "SUPPORT_PRIORITY_INVALID", "The selected support priority is not active.");
        if (statusId.HasValue && !await db.SupportTicketStatuses.AnyAsync(x => x.Id == statusId && x.IsActive, cancellationToken))
            throw new SupportOperationException(400, "SUPPORT_STATUS_INVALID", "The selected support status is not active.");
    }

    private static async ValueTask ValidateAttachmentsAsync(AppDbContext db, IReadOnlyList<Guid> attachmentIds, CancellationToken cancellationToken)
    {
        if (attachmentIds.Count == 0) return;

        int count = await db.Files.CountAsync(x => attachmentIds.Contains(x.Id) && !x.IsDeleted, cancellationToken);

        if (count != attachmentIds.Count) throw new SupportOperationException(400, "SUPPORT_ATTACHMENT_INVALID", "One or more attachment files do not exist or are deleted.");
    }

    private static IReadOnlyList<Guid> NormalizeAttachmentIds(IReadOnlyCollection<Guid>? values)
    {
        if (values is null || values.Count == 0) return [];

        Guid[] result = values.Where(x => x != Guid.Empty).Distinct().ToArray();

        if (result.Length > 20) throw new SupportOperationException(400, "SUPPORT_TOO_MANY_ATTACHMENTS", "A support message may contain at most 20 attachments.");
        
        return result;
    }

    private static void ValidateSubject(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new SupportOperationException(400, "SUPPORT_SUBJECT_REQUIRED", "A support ticket subject is required.");
        if (value.Trim().Length > 250) throw new SupportOperationException(400, "SUPPORT_SUBJECT_TOO_LONG", "The support ticket subject exceeds 250 characters.");
    }

    private static void ValidateMessage(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new SupportOperationException(400, "SUPPORT_MESSAGE_REQUIRED", "A support message is required.");
        if (value.Trim().Length > 20000) throw new SupportOperationException(400, "SUPPORT_MESSAGE_TOO_LONG", "The support message exceeds 20000 characters.");
    }
}
