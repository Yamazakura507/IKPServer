namespace IKP.Domain.Common.Interfaces.Localized
{
    public interface IHasLocalizedName
    {
        /// <summary>
        /// Gets or sets the localization key for the source name.
        /// </summary>
        string NameKey { get; set; }
    }
}
