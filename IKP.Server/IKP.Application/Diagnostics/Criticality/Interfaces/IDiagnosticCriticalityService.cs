namespace IKP.Application.Diagnostics.Criticality.Interfaces
{
    /// <summary>
    /// Calculates and persists diagnostic criticality.
    /// </summary>
    public interface IDiagnosticCriticalityService
    {
        /// <summary>
        /// Recalculates criticality for an error group.
        /// </summary>
        /// <param name="errorGroupId">Error group identifier.</param>
        /// <param name="calculatedAt">Calculation timestamp.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        ValueTask RecalculateAsync(Guid errorGroupId, DateTime calculatedAt, CancellationToken cancellationToken = default);
    }
}
