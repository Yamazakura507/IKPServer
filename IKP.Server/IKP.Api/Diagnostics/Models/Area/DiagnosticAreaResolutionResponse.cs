namespace IKP.Api.Diagnostics.Models.Area
{
    /// <summary>
    /// Represents a persisted diagnostic area resolution.
    /// </summary>
    public sealed class DiagnosticAreaResolutionResponse
    {
        public Guid Id { get; init; }
        public Guid? DiagnosticAreaId { get; init; }
        public string? DiagnosticAreaCode { get; init; }
        public Guid? SourceId { get; init; }
        public string? SourceCode { get; init; }
        public Guid? RuleId { get; init; }
        public string? RuleCode { get; init; }
        public decimal Confidence { get; init; }
        public int Version { get; init; }
    }
}