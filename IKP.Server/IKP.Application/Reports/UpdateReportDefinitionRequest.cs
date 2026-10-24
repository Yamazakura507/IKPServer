namespace IKP.Application.Reports
{
    public sealed class UpdateReportDefinitionRequest 
    { 
        public Guid CategoryId { get; set; } 
        public string NameKey { get; set; } = null!;
        public string? DescriptionKey { get; set; }
        public bool IsHtmlDescription { get; set; } 
        public bool IsActive { get; set; } 
    }
}
