namespace IKP.Api.Diagnostics.Models.Area
{
    /// <summary>
    /// Represents a diagnostic area resolution rule lookup value.
    /// </summary>
    public sealed class DiagnosticAreaResolutionRuleOptionResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; } = null!;
    }
}
