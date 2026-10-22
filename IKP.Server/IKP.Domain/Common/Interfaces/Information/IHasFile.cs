using IKP.Domain.Entities.Files;

namespace IKP.Domain.Common.Interfaces.Information
{
    public interface IHasFile
    {
        /// <summary>
        /// Gets or sets the associated file identifier.
        /// </summary>
        Guid? FileId { get; set; }

        /// <summary>
        /// Gets or sets the associated file
        /// </summary>
        public StoredFile? File { get; set; }
    }
}
