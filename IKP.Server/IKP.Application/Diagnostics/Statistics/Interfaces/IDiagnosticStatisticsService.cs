namespace IKP.Application.Diagnostics.Statistics.Interfaces
{
    /// <summary>
    /// Recalculates persisted diagnostic statistics for an error group.
    /// </summary>
    public interface IDiagnosticStatisticsService
    {
        /// <summary>
        /// Recalculates metric buckets and aggregate statistics for an error group.
        /// </summary>
        /// <param name="errorGroupId">Error group identifier.</param>
        /// <param name="calculatedAt">Timestamp used for the calculation.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous calculation.</returns>
        ValueTask RecalculateAsync(Guid errorGroupId, DateTime calculatedAt, CancellationToken cancellationToken = default);
    }
}
