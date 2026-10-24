namespace IKP.Application.Reports
{
    public sealed class ReportParameterResponse 
    {
        public Guid Id { get; set; } 
        public Guid ReportDefinitionId { get; set; } 
        public string Code { get; set; } = null!; 
        public string NameKey { get; set; } = null!; 
        public Guid DataTypeCodeId { get; set; } 
        public bool IsRequired { get; set; } 
        public string? DefaultValue { get; set; } 
        public int SortOrder { get; set; } 
        public bool IsActive { get; set; } 
    }
}
