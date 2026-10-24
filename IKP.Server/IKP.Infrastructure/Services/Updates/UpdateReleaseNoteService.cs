using IKP.Application.Updates;
using IKP.Application.Updates.Interfaces;
using IKP.Domain.Entities.Updates;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.Updates;

public sealed class UpdateReleaseNoteService(AppDbContext db) : IUpdateReleaseNoteService
{
    public async ValueTask<UpdateReleaseNoteResponse> CreateAsync(Guid versionId, CreateReleaseNoteRequest request, CancellationToken cancellationToken = default)
    {
        if(!await db.UpdateVersions.AnyAsync(x => x.Id == versionId, cancellationToken)) throw new KeyNotFoundException();

        UpdateReleaseNote releaseNote = new () 
        {
            VersionId = versionId,
            SortOrder = request.SortOrder,
            Title = request.Title.Trim(),
            Description = request.Description,
            IsPlanned = request.IsPlanned,
            IsHtmlDescription = request.IsHtmlDescription
        };
        
        db.UpdateReleaseNotes.Add(releaseNote);
        await db.SaveChangesAsync(cancellationToken);

        return To(releaseNote);
    }
   
    public async ValueTask<UpdateReleaseNoteResponse> UpdateAsync(Guid id, UpdateReleaseNoteRequest request, CancellationToken cancellationToken = default)
    {
        UpdateReleaseNote releaseNote = await db.UpdateReleaseNotes.SingleOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException();
        
        releaseNote.SortOrder = request.SortOrder;
        releaseNote.Title = request.Title.Trim();
        releaseNote.Description = request.Description;
        releaseNote.IsPlanned = request.IsPlanned;
        releaseNote.IsHtmlDescription = request.IsHtmlDescription;
        
        await db.SaveChangesAsync(cancellationToken);
        
        return To(releaseNote);
    }
   
    public async ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        UpdateReleaseNote releaseNote = await db.UpdateReleaseNotes.SingleOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException();

        db.UpdateReleaseNotes.Remove(releaseNote);
        await db.SaveChangesAsync(cancellationToken);
    }
    
    private static UpdateReleaseNoteResponse To(UpdateReleaseNote releaseNote) => new ()
    {
        Id = releaseNote.Id,
        VersionId = releaseNote.VersionId,
        SortOrder = releaseNote.SortOrder,
        Title = releaseNote.Title,
        Description = releaseNote.Description,
        IsPlanned = releaseNote.IsPlanned,
        IsHtmlDescription = releaseNote.IsHtmlDescription
    };
}
