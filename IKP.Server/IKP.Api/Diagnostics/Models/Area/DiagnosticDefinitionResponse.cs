namespace IKP.Api.Diagnostics.Models.Area
{
    /// <summary>
    /// Represents a compact diagnostic definition read model.
    /// </summary>
    public sealed class DiagnosticDefinitionResponse
    {
        public Guid Id { get; init; }
        public Guid GroupId { get; init; }
        public string? GroupCode { get; init; }
        public string Code { get; init; } = null!;
        public string NameKey { get; init; } = null!;
        public string? DescriptionKey { get; init; }
        public Guid? FileId { get; init; }
        public int SortOrder { get; init; }
        public bool IsActive { get; init; }
        public bool IsSystem { get; init; }
    }
}