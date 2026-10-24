namespace IKP.Application.Updates
{
    public sealed class UpdateReleaseNoteRequest
    { 
        public int SortOrder { get; set; } 
        public string Title { get; set; } = null!; 
        public string? Description { get; set; } 
        public bool IsPlanned { get; set; } 
        public bool IsHtmlDescription { get; set; }
    }
}
