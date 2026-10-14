namespace IKP.Api.Diagnostics.Models.Statistics
{
    /// <summary>
    /// Represents one persisted diagnostic metric bucket.
    /// </summary>
    public sealed class DiagnosticErrorMetricBucketResponse
    {
        public Guid Id { get; init; }
        public Guid ErrorGroupId { get; init; }
        public DateTime BucketStart { get; init; }
        public int BucketDurationMinutes { get; init; }
        public Guid BucketTypeId { get; init; }
        public string? BucketTypeCode { get; init; }
        public long Occurrences { get; init; }
        public long UniqueUsers { get; init; }
        public DateTime? FirstOccurrenceAt { get; init; }
        public DateTime? LastOccurrenceAt { get; init; }
        public DateTime CalculatedAt { get; init; }
    }
}