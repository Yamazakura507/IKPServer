namespace IKP.Application.Reports
{
    public sealed class UpdateReportTemplateRequest 
    { 
        public Guid FileId { get; set; } 
        public string? ContentHash { get; set; }
        public long? Size { get; set; } 
        public bool IsPublished { get; set; } 
        public bool IsActive { get; set; } 
    }
}
