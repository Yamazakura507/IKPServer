using IKP.Application.Support.Models;

namespace IKP.Application.Support.Interfaces;

/// <summary>
/// Provides support ticket lifecycle and conversation operations.
/// </summary>
public interface ISupportService
{
    /// <summary>
    /// Creates a new support ticket and its initial message.
    /// </summary>
    ValueTask<SupportTicketResponse> CreateTicketAsync(CreateSupportTicketRequest request, Guid actorUserId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a ticket with its visible messages.
    /// </summary>
    ValueTask<object?> GetTicketAsync(Guid ticketId, bool includeInternalNotes, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates ticket metadata.
    /// </summary>
    ValueTask<SupportTicketResponse> UpdateTicketAsync(Guid ticketId, UpdateSupportTicketRequest request, Guid actorUserId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Changes ticket status.
    /// </summary>
    ValueTask<SupportTicketResponse> SetStatusAsync(Guid ticketId, SetSupportTicketStatusRequest request, Guid actorUserId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Changes ticket assignment.
    /// </summary>
    ValueTask<SupportTicketResponse> AssignAsync(Guid ticketId, AssignSupportTicketRequest request, Guid actorUserId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Adds a public message.
    /// </summary>
    ValueTask<SupportTicketMessageResponse> AddMessageAsync(Guid ticketId, AddSupportTicketMessageRequest request, Guid actorUserId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Adds an internal note.
    /// </summary>
    ValueTask<SupportTicketMessageResponse> AddInternalNoteAsync(Guid ticketId, AddSupportInternalNoteRequest request, Guid actorUserId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Soft-deletes a ticket.
    /// </summary>
    ValueTask DeleteAsync(Guid ticketId, Guid actorUserId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Restores a soft-deleted ticket.
    /// </summary>
    ValueTask RestoreAsync(Guid ticketId, Guid actorUserId, CancellationToken cancellationToken = default);
}
