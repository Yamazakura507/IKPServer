namespace IKP.Application.Reports
{
    public sealed class ReportTemplateResponse 
    { 
        public Guid Id { get; set; } 
        public Guid ReportDefinitionId { get; set; } 
        public Guid FileId { get; set; } 
        public int Version { get; set; } 
        public string? ContentHash { get; set; } 
        public long? Size { get; set; }
        public bool IsPublished { get; set; } 
        public bool IsActive { get; set; } 
        public DateTime? PublishedAt { get; set; } 
    }
}
