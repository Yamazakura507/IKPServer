namespace IKP.Application.Updates
{
    public sealed class AddAttachmentRequest
    {
        public Guid FileId { get; set; }
        public int SortOrder { get; set; }
    }
}
