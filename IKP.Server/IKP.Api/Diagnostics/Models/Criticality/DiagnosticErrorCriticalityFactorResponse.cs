namespace IKP.Api.Diagnostics.Models.Criticality
{
    /// <summary>
    /// Represents one persisted criticality factor contribution.
    /// </summary>
    public sealed class DiagnosticErrorCriticalityFactorResponse
    {
        public Guid Id { get; init; }
        public Guid DefinitionId { get; init; }
        public string? DefinitionCode { get; init; }
        public decimal Score { get; init; }
        public decimal Weight { get; init; }
        public decimal Contribution { get; init; }
        public decimal? Value { get; init; }
        public DateTime CalculatedAt { get; init; }
    }
}