namespace IKP.Api.Diagnostics.Models.Criticality
{
    /// <summary>
    /// Represents persisted automatic and manual criticality data.
    /// </summary>
    public sealed class DiagnosticErrorCriticalityResponse
    {
        public Guid Id { get; init; }
        public Guid ErrorGroupId { get; init; }
        public string? ErrorTitle { get; init; }
        public decimal AutomaticScore { get; init; }
        public decimal? ManualScore { get; init; }
        public decimal EffectiveScore { get; init; }
        public int CalculationVersion { get; init; }
        public DateTime CalculatedAt { get; init; }
        public Guid? OverrideUserId { get; init; }
        public DateTime? OverrideAt { get; init; }
        public string? OverrideReason { get; init; }
        public IReadOnlyList<DiagnosticErrorCriticalityFactorResponse>? Factors { get; init; }
    }
}