namespace IKP.Application.Updates
{
    public sealed class UpdateProductResponse 
    { 
        public Guid Id { get; set; } 
        public Guid CategoryId { get; set; } 
        public Guid? PlatformId { get; set; } 
        public string Code { get; set; } = null!; 
        public string NameKey { get; set; } = null!; 
        public string? DescriptionKey { get; set; } 
        public Guid? FileId { get; set; } 
        public bool IsActive { get; set; } 
    }
}
