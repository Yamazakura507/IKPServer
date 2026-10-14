namespace IKP.Api.Diagnostics.Correlation.Interfaces
{
    /// <summary>
    /// Provides access to the correlation ID of the current HTTP request.
    /// </summary>
    public interface ICorrelationIdAccessor
    {
        /// <summary>
        /// Gets the correlation ID of the current request.
        /// </summary>
        string? CorrelationId { get; }
    }
}
