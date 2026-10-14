namespace IKP.Api.Diagnostics.Models.Area
{
    /// <summary>
    /// Represents a diagnostic area lookup value.
    /// </summary>
    public sealed class DiagnosticAreaOptionResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; } = null!;
    }
}
