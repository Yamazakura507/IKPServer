namespace IKP.Application.Updates
{
    public sealed class AddDownloadLinkRequest
    {
        public string Url { get; set; } = null!;
        public string? Name { get; set; }
        public bool IsPrimary { get; set; }
    }
}
