namespace IKP.Application.Updates
{
    public sealed class CreateProductRequest
    {
        public Guid CategoryId { get; set; }
        public Guid? PlatformId { get; set; }
        public string Code { get; set; } = null!;
        public string NameKey { get; set; } = null!;
        public string? DescriptionKey { get; set; }
    }
}
