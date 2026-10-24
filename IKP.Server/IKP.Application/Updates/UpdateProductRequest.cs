namespace IKP.Application.Updates
{
    public sealed class UpdateProductRequest
    { 
        public Guid CategoryId { get; set; } 
        public Guid? PlatformId { get; set; }
        public string NameKey { get; set; } = null!;
        public string? DescriptionKey { get; set; } 
        public Guid? FileId { get; set; } 
        public bool IsActive { get; set; } 
    }
}
