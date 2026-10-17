namespace IKP.Domain.Common.Interfaces.Audit
{
    public interface ISoftDelete
    {
        /// <summary>
        /// Deletion marker
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the last deleted timestamp in UTC.
        /// </summary>
        DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Gets or sets that last deleted the assignment.
        /// </summary>
        Guid? DeletedBy { get; set; }
    }
}
