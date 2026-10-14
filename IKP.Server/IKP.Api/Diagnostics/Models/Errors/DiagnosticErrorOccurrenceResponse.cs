namespace IKP.Api.Diagnostics.Models.Errors
{
    /// <summary>
    /// Represents the compact diagnostic error-occurrence view.
    /// </summary>
    public sealed class DiagnosticErrorOccurrenceResponse
    {
        public Guid Id { get; init; }
        public Guid ErrorGroupId { get; init; }
        public DateTime OccurredAt { get; init; }
        public Guid? UserId { get; init; }
        public string? SessionId { get; init; }
        public string? ApplicationId { get; init; }
        public string? ApplicationVersion { get; init; }
        public string? Platform { get; init; }
        public string? MachineId { get; init; }
        public string? ClientInstanceId { get; init; }
        public string? RequestId { get; init; }
        public string? CorrelationId { get; init; }
        public string? Endpoint { get; init; }
        public string? HttpMethod { get; init; }
        public string? Route { get; init; }
        public string? ExceptionType { get; init; }
        public string? Message { get; init; }
        public string? SourceFile { get; init; }
        public string? SourceMethod { get; init; }
        public int? SourceLineNumber { get; init; }
    }
}