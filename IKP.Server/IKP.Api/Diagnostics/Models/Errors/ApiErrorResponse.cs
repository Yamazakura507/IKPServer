namespace IKP.Api.Diagnostics.Models.Errors
{
    /// <summary>
    /// Represents a standardized API error response.
    /// </summary>
    public class ApiErrorResponse
    {
        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the machine-readable error code.
        /// </summary>
        public string Code { get; set; } = null!;

        /// <summary>
        /// Gets or sets the human-readable error title.
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        public string RequestId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the correlation identifier.
        /// </summary>
        public string CorrelationId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the diagnostic error group identifier.
        /// </summary>
        public Guid? ErrorGroupId { get; set; }
    }
}
