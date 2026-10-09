namespace IKP.Application.Diagnostics.Models
{
    /// <summary>
    /// Contains contextual information required to register a diagnostic error.
    /// </summary>
    public class DiagnosticErrorContext
    {
        /// <summary>
        /// Gets or sets the user identifier associated with the error.
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Gets or sets the diagnostic session identifier.
        /// </summary>
        public string? SessionId { get; set; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        public string? ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the application version.
        /// </summary>
        public string? ApplicationVersion { get; set; }

        /// <summary>
        /// Gets or sets the platform on which the error occurred.
        /// </summary>
        public string? Platform { get; set; }

        /// <summary>
        /// Gets or sets the machine identifier.
        /// </summary>
        public string? MachineId { get; set; }

        /// <summary>
        /// Gets or sets the client instance identifier.
        /// </summary>
        public string? ClientInstanceId { get; set; }

        /// <summary>
        /// Gets or sets the HTTP request identifier.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Gets or sets the correlation identifier.
        /// </summary>
        public string? CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the endpoint associated with the error.
        /// </summary>
        public string? Endpoint { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method associated with the error.
        /// </summary>
        public string? HttpMethod { get; set; }

        /// <summary>
        /// Gets or sets the route associated with the error.
        /// </summary>
        public string? Route { get; set; }

        /// <summary>
        /// Gets or sets the diagnostic area identifier.
        /// </summary>
        public Guid? DiagnosticAreaId { get; set; }

        /// <summary>
        /// Gets or sets the optional application-specific error code.
        /// </summary>
        public string? ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets additional diagnostic context.
        /// </summary>
        public object? Context { get; set; }
    }
}
