namespace IKP.Application.Updates.Interfaces;

public interface IUpdateReleaseNoteService
{ 
    ValueTask<UpdateReleaseNoteResponse> CreateAsync(Guid versionId, CreateReleaseNoteRequest request, CancellationToken cancellationToken = default); 
    ValueTask<UpdateReleaseNoteResponse> UpdateAsync(Guid id, UpdateReleaseNoteRequest request, CancellationToken cancellationToken = default); 
    ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default); 
}
