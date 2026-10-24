namespace IKP.Application.Reports
{
    public sealed class ReportDefinitionResponse 
    { 
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; } 
        public string Code { get; set; } = null!; 
        public string NameKey { get; set; } = null!;
        public string? DescriptionKey { get; set; } 
        public bool IsHtmlDescription { get; set; } 
        public bool IsActive { get; set; } 
        public bool IsSystem { get; set; } 
    }
}
