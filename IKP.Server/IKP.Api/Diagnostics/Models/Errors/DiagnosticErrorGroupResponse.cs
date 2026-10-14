namespace IKP.Api.Diagnostics.Models.Errors
{
    /// <summary>
    /// Represents the compact diagnostic error-group view.
    /// </summary>
    public sealed class DiagnosticErrorGroupResponse
    {
        public Guid Id { get; init; }
        public string Fingerprint { get; init; } = null!;
        public int FingerprintVersion { get; init; }
        public string ApplicationId { get; init; } = null!;
        public Guid? DiagnosticAreaId { get; init; }
        public string? DiagnosticAreaCode { get; init; }
        public string ExceptionType { get; init; } = null!;
        public string? ErrorCode { get; init; }
        public string Title { get; init; } = null!;
        public string? Summary { get; init; }
        public Guid SeverityId { get; init; }
        public string? SeverityCode { get; init; }
        public Guid StatusId { get; init; }
        public string? StatusCode { get; init; }
        public DateTime FirstOccurredAt { get; init; }
        public DateTime LastOccurredAt { get; init; }
        public string? FirstSeenVersion { get; init; }
        public string? LastSeenVersion { get; init; }
        public long? TotalOccurrences { get; init; }
        public long? UniqueUsers { get; init; }
        public decimal? AffectedUserRatio { get; init; }
        public decimal? CriticalityScore { get; init; }
        public bool HasManualCriticalityOverride { get; init; }
    }
}