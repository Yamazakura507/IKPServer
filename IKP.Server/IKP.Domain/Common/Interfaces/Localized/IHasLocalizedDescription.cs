namespace IKP.Domain.Common.Interfaces.Localized
{
    public interface IHasLocalizedDescription
    {
        /// <summary>
        /// Gets or sets the localization key for the source description.
        /// </summary>
        string? DescriptionKey { get; set; }
    }
}
