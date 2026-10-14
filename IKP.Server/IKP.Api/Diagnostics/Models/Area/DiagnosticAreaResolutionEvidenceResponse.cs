namespace IKP.Api.Diagnostics.Models.Area
{
    /// <summary>
    /// Represents immutable evidence captured during diagnostic area resolution.
    /// </summary>
    public sealed class DiagnosticAreaResolutionEvidenceResponse
    {
        public Guid Id { get; init; }
        public Guid DiagnosticAreaResolutionId { get; init; }
        public Guid? MappingId { get; init; }
        public Guid RuleId { get; init; }
        public string? RuleCode { get; init; }
        public string Pattern { get; init; } = null!;
        public string MatchedValue { get; init; } = null!;
        public int Priority { get; init; }
        public decimal Confidence { get; init; }
        public decimal Contribution { get; init; }
    }
}