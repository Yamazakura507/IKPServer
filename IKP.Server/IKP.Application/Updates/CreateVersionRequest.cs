namespace IKP.Application.Updates
{
    public sealed class CreateVersionRequest
    {
        public string Version { get; set; } = null!;
        public Guid? FileId { get; set; }
        public string? ContentHash { get; set; }
        public long? Size { get; set; }
        public bool IsPrerelease { get; set; }
        public bool IsPublished { get; set; }
        public bool IsRequired { get; set; }
        public DateTime? ReleaseAt { get; set; }
    }
}
