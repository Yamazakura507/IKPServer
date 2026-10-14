namespace IKP.Api.Diagnostics.Models.Area
{
    /// <summary>
    /// Represents the compact read model for a diagnostic lookup.
    /// </summary>
    public sealed class DiagnosticLookupResponse
    {
        public Guid Id { get; init; }
        public string Code { get; init; } = null!;
        public string NameKey { get; init; } = null!;
        public string? DescriptionKey { get; init; }
        public Guid? FileId { get; init; }
        public int SortOrder { get; init; }
        public bool IsActive { get; init; }
        public bool IsSystem { get; init; }
        public int? ImportanceScore { get; init; }
    }
}