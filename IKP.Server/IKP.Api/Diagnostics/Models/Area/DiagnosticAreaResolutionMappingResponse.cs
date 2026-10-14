namespace IKP.Api.Diagnostics.Models.Area
{
    /// <summary>
    /// Represents a diagnostic area resolution mapping returned by the API.
    /// </summary>
    public sealed class DiagnosticAreaResolutionMappingResponse
    {
        /// <summary>
        /// Gets or sets the mapping identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the diagnostic area identifier.
        /// </summary>
        public Guid DiagnosticAreaId { get; set; }

        /// <summary>
        /// Gets or sets the diagnostic area code.
        /// </summary>
        public string DiagnosticAreaCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the resolution rule identifier.
        /// </summary>
        public Guid RuleId { get; set; }

        /// <summary>
        /// Gets or sets the resolution rule code.
        /// </summary>
        public string RuleCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the regular expression pattern.
        /// </summary>
        public string Pattern { get; set; } = null!;

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the base confidence.
        /// </summary>
        public decimal Confidence { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mapping is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mapping is system-defined.
        /// </summary>
        public bool IsSystem { get; set; }
    }
}
