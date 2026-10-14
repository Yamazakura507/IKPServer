namespace IKP.Api.Diagnostics.Models.Area
{
    /// <summary>
    /// Provides lookup values required to create and edit diagnostic area resolution mappings.
    /// </summary>
    public sealed class DiagnosticAreaResolutionMappingOptionsResponse
    {
        /// <summary>
        /// Gets or sets available diagnostic areas.
        /// </summary>
        public IReadOnlyList<DiagnosticAreaOptionResponse> Areas { get; set; } = [];

        /// <summary>
        /// Gets or sets available resolution rules.
        /// </summary>
        public IReadOnlyList<DiagnosticAreaResolutionRuleOptionResponse> Rules { get; set; } = [];
    }
}
