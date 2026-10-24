using System.Linq.Expressions;
using System.Security.Claims;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Auth.Authorization;
using IKP.Application.Common.Query;
using IKP.Application.Support.Interfaces;
using IKP.Application.Support.Models;
using IKP.Domain.Common.Auth;
using IKP.Domain.Entities.Support;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Support;

/// <summary>
/// Provides support ticket lifecycle and conversation operations.
/// </summary>
[ApiController]
[Route("api/support/tickets")]
[Authorize(Policy = AuthorizationPolicyNames.SupportRead)]
public sealed class SupportTicketsController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<SupportTicket, object?>>> Fields = new Dictionary<string, Expression<Func<SupportTicket, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id, 
        ["number"] = x => x.Number, 
        ["subject"] = x => x.Subject, 
        ["createdByUserId"] = x => x.CreatedByUserId,
        ["categoryId"] = x => x.CategoryId, 
        ["categoryCode"] = x => x.Category.Code, 
        ["priorityId"] = x => x.PriorityId, 
        ["priorityCode"] = x => x.Priority.Code,
        ["statusId"] = x => x.StatusId, 
        ["statusCode"] = x => x.Status.Code, 
        ["assignedToUserId"] = x => x.AssignedToUserId,
        ["lastMessageAt"] = x => x.LastMessageAt, 
        ["resolvedAt"] = x => x.ResolvedAt, 
        ["closedAt"] = x => x.ClosedAt, 
        ["createdAt"] = x => x.CreatedAt, 
        ["updatedAt"] = x => x.UpdatedAt
    };

    private readonly ISupportService service;
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    /// <summary>
    /// Initializes the support ticket controller.
    /// </summary>
    public SupportTicketsController(ISupportService service, IDbContextFactory<AppDbContext> dbContextFactory)
    {
        this.service = service;
        this.dbContextFactory = dbContextFactory;
    }

    /// <summary>
    /// Queries support tickets using the universal query engine.
    /// </summary>
    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.SupportTickets.AsNoTracking().Where(x => !x.IsDeleted).ToPagedResponseAsync(request, Fields, x => new
        {
            x.Id, 
            x.Number, 
            x.Subject, 
            x.CreatedByUserId, 
            x.CategoryId, 
            CategoryCode = x.Category.Code, 
            x.PriorityId, 
            PriorityCode = x.Priority.Code,
            x.StatusId, 
            StatusCode = x.Status.Code, 
            x.AssignedToUserId, 
            x.LastMessageAt, 
            x.ResolvedAt, 
            x.ClosedAt, 
            x.CreatedAt, 
            x.UpdatedAt
        }, cancellationToken));
    }

    /// <summary>
    /// Gets a support ticket and its visible conversation.
    /// </summary>
    [HttpGet("{ticketId:guid}")]
    public async Task<IActionResult> Get(Guid ticketId, CancellationToken cancellationToken)
    {
        bool includeInternalNotes = User.HasClaim("permission", AuthorizationPermissionCodes.SupportInternalNotesRead);
        object? result = await service.GetTicketAsync(ticketId, includeInternalNotes, cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Creates a support ticket.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicyNames.SupportCreate)]
    public async Task<ActionResult<SupportTicketResponse>> Create([FromBody] CreateSupportTicketRequest request, CancellationToken cancellationToken)
    {
        SupportTicketResponse response = await service.CreateTicketAsync(request, GetRequiredUserId(), cancellationToken);

        return CreatedAtAction(nameof(Get), new { ticketId = response.Id }, response);
    }

    /// <summary>
    /// Updates ticket subject, category and priority.
    /// </summary>
    [HttpPut("{ticketId:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.SupportUpdate)]
    public async Task<ActionResult<SupportTicketResponse>> Update(Guid ticketId, [FromBody] UpdateSupportTicketRequest request, CancellationToken cancellationToken) => Ok(await service.UpdateTicketAsync(ticketId, request, GetRequiredUserId(), cancellationToken));

    /// <summary>
    /// Changes the ticket status.
    /// </summary>
    [HttpPut("{ticketId:guid}/status")]
    [Authorize(Policy = AuthorizationPolicyNames.SupportStatusManage)]
    public async Task<ActionResult<SupportTicketResponse>> SetStatus(Guid ticketId, [FromBody] SetSupportTicketStatusRequest request, CancellationToken cancellationToken) => Ok(await service.SetStatusAsync(ticketId, request, GetRequiredUserId(), cancellationToken));

    /// <summary>
    /// Assigns or unassigns a ticket.
    /// </summary>
    [HttpPut("{ticketId:guid}/assignment")]
    [Authorize(Policy = AuthorizationPolicyNames.SupportAssign)]
    public async Task<ActionResult<SupportTicketResponse>> Assign(Guid ticketId, [FromBody] AssignSupportTicketRequest request, CancellationToken cancellationToken) => Ok(await service.AssignAsync(ticketId, request, GetRequiredUserId(), cancellationToken));

    /// <summary>
    /// Adds a public ticket message.
    /// </summary>
    [HttpPost("{ticketId:guid}/messages")]
    [Authorize(Policy = AuthorizationPolicyNames.SupportMessagesCreate)]
    public async Task<ActionResult<SupportTicketMessageResponse>> AddMessage(Guid ticketId, [FromBody] AddSupportTicketMessageRequest request, CancellationToken cancellationToken) => Ok(await service.AddMessageAsync(ticketId, request, GetRequiredUserId(), cancellationToken));

    /// <summary>
    /// Adds an internal support note.
    /// </summary>
    [HttpPost("{ticketId:guid}/internal-notes")]
    [Authorize(Policy = AuthorizationPolicyNames.SupportInternalNotesCreate)]
    public async Task<ActionResult<SupportTicketMessageResponse>> AddInternalNote(Guid ticketId, [FromBody] AddSupportInternalNoteRequest request, CancellationToken cancellationToken) => Ok(await service.AddInternalNoteAsync(ticketId, request, GetRequiredUserId(), cancellationToken));

    /// <summary>
    /// Soft-deletes a support ticket.
    /// </summary>
    [HttpDelete("{ticketId:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.SupportDelete)]
    public async Task<IActionResult> Delete(Guid ticketId, CancellationToken cancellationToken) 
    { 
        await service.DeleteAsync(ticketId, GetRequiredUserId(), cancellationToken); 
        
        return NoContent(); 
    }

    /// <summary>
    /// Restores a deleted support ticket.
    /// </summary>
    [HttpPost("{ticketId:guid}/restore")]
    [Authorize(Policy = AuthorizationPolicyNames.SupportUpdate)]
    public async Task<IActionResult> Restore(Guid ticketId, CancellationToken cancellationToken) 
    { 
        await service.RestoreAsync(ticketId, GetRequiredUserId(), cancellationToken); 
        
        return NoContent(); 
    }

    private Guid GetRequiredUserId()
    {
        string? value = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(value, out Guid userId)) throw new InvalidOperationException("The authenticated user identifier is missing.");

        return userId;
    }
}
