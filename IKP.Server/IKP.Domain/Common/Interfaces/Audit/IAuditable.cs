namespace IKP.Domain.Common.Interfaces.Audit
{
    public interface IAuditable
    {
        /// <summary>
        /// Gets or sets the grant timestamp in UTC.
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the user that created.
        /// </summary>
        Guid? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the last update timestamp in UTC.
        /// </summary>
        DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets that last updated the assignment.
        /// </summary>
        Guid? UpdatedBy { get; set; }
    }
}
