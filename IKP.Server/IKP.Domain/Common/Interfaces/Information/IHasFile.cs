namespace IKP.Domain.Common.Interfaces.Information
{
    public interface IHasFile
    {
        /// <summary>
        /// Gets or sets the associated file identifier.
        /// </summary>
        Guid? FileId { get; set; }
    }
}
