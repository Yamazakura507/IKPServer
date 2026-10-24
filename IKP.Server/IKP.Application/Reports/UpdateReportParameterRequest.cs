namespace IKP.Application.Reports
{
    public sealed class UpdateReportParameterRequest 
    { 
        public string NameKey { get; set; } = null!; 
        public Guid DataTypeCodeId { get; set; } 
        public bool IsRequired { get; set; } 
        public string? DefaultValue { get; set; } 
        public int SortOrder { get; set; } 
        public bool IsActive { get; set; } 
    }
}
