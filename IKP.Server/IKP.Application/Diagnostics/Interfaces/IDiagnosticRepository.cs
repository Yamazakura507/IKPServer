using IKP.Domain.Entities.Diagnostics.Errors;

namespace IKP.Application.Diagnostics.Interfaces
{
    /// <summary>
    /// Provides persistence operations for the diagnostic subsystem.
    /// </summary>
    public interface IDiagnosticRepository
    {
        /// <summary>
        /// Finds an error group by its fingerprint.
        /// </summary>
        /// <param name="fingerprint">Error fingerprint.</param>
        /// <param name="fingerprintVersion">Fingerprint algorithm version.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching error group or null.</returns>
        ValueTask<ErrorGroup?> FindErrorGroupAsync(string fingerprint, int fingerprintVersion, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a diagnostic definition identifier by group and definition codes.
        /// </summary>
        /// <param name="groupCode">Diagnostic definition group code.</param>
        /// <param name="definitionCode">Diagnostic definition code.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The definition identifier.</returns>
        ValueTask<Guid> GetDefinitionIdAsync(string groupCode, string definitionCode, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new error group.
        /// </summary>
        /// <param name="errorGroup">Error group to add.</param>
        void AddErrorGroup(ErrorGroup errorGroup);

        /// <summary>
        /// Adds a new error occurrence.
        /// </summary>
        /// <param name="errorOccurrence">Error occurrence to add.</param>
        void AddErrorOccurrence(ErrorOccurrence errorOccurrence);

        /// <summary>
        /// Saves diagnostic changes.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous save operation.</returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
