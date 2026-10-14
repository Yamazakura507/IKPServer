using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Domain.Entities.Diagnostics.Errors;
using IKP.Application.Diagnostics.Resolvers.Models;
using IKP.Domain.Entities.Diagnostics.Statistics;
using IKP.Domain.Entities.Diagnostics.Criticality;
using IKP.Application.Diagnostics.Criticality.Models;

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
        /// <param name="groupCode">Diagnostic definition group code</param>
        /// <param name="definitionCode">Diagnostic definition code</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The definition identifier.</returns>
        ValueTask<Guid> GetDefinitionIdAsync(string groupCode, string definitionCode, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a diagnostic area identifier by group and areas codes
        /// </summary>
        /// <param name="code">Diagnostic area code</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        ValueTask<Guid> GetDiagnosticAreaIdAsync(string code, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets active automatic diagnostic area resolution mappings.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Active resolution mappings.</returns>
        ValueTask<IReadOnlyList<DiagnosticAreaResolutionMappingModel>> GetActiveDiagnosticAreaResolutionMappingsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates an automatically resolved diagnostic area resolution and its evidence.
        /// </summary>
        /// <param name="diagnosticAreaId">Resolved diagnostic area identifier.</param>
        /// <param name="primaryRuleId">Primary rule identifier.</param>
        /// <param name="confidence">Aggregate resolution confidence.</param>
        /// <param name="version">Resolution algorithm version.</param>
        /// <param name="evidences">Matched evidence records.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created resolution.</returns>
        ValueTask<DiagnosticAreaResolution> CreateAutomaticDiagnosticAreaResolutionAsync(
            Guid diagnosticAreaId, Guid primaryRuleId, decimal confidence, int version, IReadOnlyList<DiagnosticAreaResolutionEvidenceModel> evidences, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a diagnostic area resolution.
        /// </summary>
        /// <param name="diagnosticAreaId">Resolved diagnostic area identifier.</param>
        /// <param name="sourceCode">Resolution source code.</param>
        /// <param name="ruleCode">Resolution rule code.</param>
        /// <param name="confidence">Resolution confidence from 0 to 100.</param>
        /// <param name="version">Resolution algorithm version.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created diagnostic area resolution.</returns>
        ValueTask<DiagnosticAreaResolution?> CreateDiagnosticAreaResolutionAsync(Guid? diagnosticAreaId, string sourceCode, string ruleCode, decimal confidence, int version, CancellationToken cancellationToken = default);

        /// <summary>
        /// Schedules durable statistics recalculation for an error group.
        /// </summary>
        /// <param name="errorGroupId">Error group identifier.</param>
        /// <param name="requestedAt">Timestamp of the newest request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the scheduling operation.</returns>
        ValueTask ScheduleStatisticsWorkItemAsync(Guid errorGroupId, DateTime requestedAt, CancellationToken cancellationToken = default);

        /// <summary>
        /// Claims a batch of pending statistics work items.
        /// </summary>
        /// <param name="batchSize">Maximum number of work items to claim.</param>
        /// <param name="now">Current UTC timestamp.</param>
        /// <param name="staleAfter">Maximum processing duration before a work item is recovered.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Claimed work items.</returns>
        ValueTask<IReadOnlyList<DiagnosticStatisticsWorkItem>> ClaimStatisticsWorkItemsAsync(int batchSize, DateTime now, TimeSpan staleAfter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Marks a statistics work item as completed unless a newer request arrived during processing.
        /// </summary>
        /// <param name="workItemId">Work item identifier.</param>
        /// <param name="claimedRequestedAt">Request timestamp captured when the item was claimed.</param>
        /// <param name="completedAt">Completion timestamp.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the persistence operation.</returns>
        ValueTask CompleteStatisticsWorkItemAsync(Guid workItemId, DateTime claimedRequestedAt, DateTime completedAt, CancellationToken cancellationToken = default);

        /// <summary>
        /// Marks a statistics work item as failed and schedules a retry.
        /// </summary>
        /// <param name="workItemId">Work item identifier.</param>
        /// <param name="claimedRequestedAt">Request timestamp captured when the item was claimed.</param>
        /// <param name="error">Failure details.</param>
        /// <param name="availableAt">Next retry timestamp.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the persistence operation.</returns>
        ValueTask FailStatisticsWorkItemAsync(Guid workItemId, DateTime claimedRequestedAt, string error, DateTime availableAt, CancellationToken cancellationToken = default);

        /// <summary>
        /// Recalculates all persisted statistics and metric buckets for an error group inside PostgreSQL.
        /// </summary>
        /// <param name="errorGroupId">Error group identifier.</param>
        /// <param name="calculatedAt">Calculation timestamp.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the database calculation.</returns>
        ValueTask RecalculateStatisticsAsync(Guid errorGroupId, DateTime calculatedAt, CancellationToken cancellationToken = default);

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
        /// Gets persisted metrics required for criticality calculation.
        /// </summary>
        /// <param name="errorGroupId">Error group identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Criticality calculation input.</returns>
        ValueTask<DiagnosticCriticalityCalculationInput> GetCriticalityCalculationInputAsync(Guid errorGroupId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Persists the calculated criticality and its factor breakdown.
        /// </summary>
        /// <param name="errorGroupId">Error group identifier.</param>
        /// <param name="calculatedAt">Calculation timestamp.</param>
        /// <param name="result">Calculated criticality.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        ValueTask SaveCriticalityAsync(Guid errorGroupId, DateTime calculatedAt, DiagnosticCriticalityCalculationResult result, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds error groups whose statistics are newer than their criticality calculation.
        /// </summary>
        ValueTask<IReadOnlyList<Guid>> GetCriticalityRefreshCandidatesAsync(int batchSize, CancellationToken cancellationToken = default);

        /// <summary>
        /// Saves diagnostic changes.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous save operation.</returns>
        ValueTask SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
