namespace IKP.Api.Diagnostics.Models.Area
{
    /// <summary>
    /// Represents a request to update a diagnostic area resolution mapping.
    /// </summary>
    public sealed class UpdateDiagnosticAreaResolutionMappingRequest
    {
        /// <summary>
        /// Gets or sets the diagnostic area identifier.
        /// </summary>
        public Guid DiagnosticAreaId { get; set; }

        /// <summary>
        /// Gets or sets the resolution rule identifier.
        /// </summary>
        public Guid RuleId { get; set; }

        /// <summary>
        /// Gets or sets the regular expression pattern.
        /// </summary>
        public string Pattern { get; set; } = null!;

        /// <summary>
        /// Gets or sets the priority from 0 to 100.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the base confidence from 0 to 100.
        /// </summary>
        public decimal Confidence { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mapping is active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
