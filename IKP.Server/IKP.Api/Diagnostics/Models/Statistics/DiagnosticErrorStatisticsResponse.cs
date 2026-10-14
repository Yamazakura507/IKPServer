namespace IKP.Api.Diagnostics.Models.Statistics
{
    /// <summary>
    /// Represents persisted aggregate statistics for an error group.
    /// </summary>
    public sealed class DiagnosticErrorStatisticsResponse
    {
        public Guid Id { get; init; }
        public Guid ErrorGroupId { get; init; }
        public string? ErrorTitle { get; init; }
        public string? ExceptionType { get; init; }
        public long TotalOccurrences { get; init; }
        public long UniqueUsers { get; init; }
        public decimal AffectedUserRatio { get; init; }
        public long OccurrencesLastHour { get; init; }
        public long OccurrencesLastDay { get; init; }
        public long OccurrencesLastWeek { get; init; }
        public long UsersLastHour { get; init; }
        public long UsersLastDay { get; init; }
        public long UsersLastWeek { get; init; }
        public decimal? GrowthRate { get; init; }
        public DateTime CalculatedAt { get; init; }
    }
}