namespace IKP.Domain.Common.Interfaces.Information
{
    public interface IHasActiveState
    {
        /// <summary>
        /// Gets or sets a value indicating whether the source is active.
        /// </summary>
        bool IsActive { get; set; }
    }
}
