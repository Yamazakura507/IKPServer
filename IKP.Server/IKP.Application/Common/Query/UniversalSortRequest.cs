namespace IKP.Application.Common.Query;

/// <summary>
/// Defines a single sort expression.
/// </summary>
public sealed class UniversalSortRequest
{
    /// <summary>
    /// Gets or sets the field name.
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether sorting is descending.
    /// </summary>
    public bool Descending { get; set; }
}
