using IKP.Application.Common.Query.Enums;

namespace IKP.Application.Common.Query;

/// <summary>
/// Defines a recursive filter group.
/// </summary>
public sealed class UniversalFilterGroupRequest
{
    /// <summary>
    /// Gets or sets the group logical operator.
    /// </summary>
    public UniversalFilterLogic Logic { get; set; } = UniversalFilterLogic.And;

    /// <summary>
    /// Gets or sets direct conditions.
    /// </summary>
    public List<UniversalFilterConditionRequest> Conditions { get; set; } = [];

    /// <summary>
    /// Gets or sets nested groups.
    /// </summary>
    public List<UniversalFilterGroupRequest> Groups { get; set; } = [];
}
