namespace IKP.Domain.Common.Interfaces.Information
{
    public interface IHasSystemState
    {
        /// <summary>
        /// Gets or sets a value indicating whether the source is system-defined.
        /// </summary>
        bool IsSystem { get; set; }
    }
}
