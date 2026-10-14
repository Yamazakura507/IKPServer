namespace IKP.Domain.Common.Interfaces.Information
{
    public interface IHasOrder
    {
        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        int SortOrder { get; set; }
    }
}
