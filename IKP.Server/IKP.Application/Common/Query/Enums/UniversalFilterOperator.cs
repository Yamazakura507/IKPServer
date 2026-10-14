namespace IKP.Application.Common.Query.Enums;

/// <summary>
/// Defines field comparison operators.
/// </summary>
public enum UniversalFilterOperator
{
    /// <summary>
    /// Equal.
    /// </summary> 
    Equal,

    /// <summary>
    /// Not equal.
    /// </summary> 
    NotEqual,

    /// <summary>
    /// Contains.
    /// </summary> 
    Contains,

    /// <summary>
    /// Starts with.
    /// </summary> 
    StartsWith,

    /// <summary>
    /// Ends with.
    /// </summary> 
    EndsWith,

    /// <summary>
    /// Greater than.
    /// </summary> 
    GreaterThan,

    /// <summary>
    /// Greater than or equal.
    /// </summary> 
    GreaterThanOrEqual,

    /// <summary>
    /// Less than.
    /// </summary> 
    LessThan,

    /// <summary>
    /// Less than or equal.
    /// </summary> 
    LessThanOrEqual,

    /// <summary>
    /// Value is in the supplied collection.
    /// </summary> 
    In,

    /// <summary>
    /// Value is not in the supplied collection.
    /// </summary> 
    NotIn,

    /// <summary>
    /// Value is null.
    /// </summary> 
    IsNull,

    /// <summary>
    /// Value is not null.
    /// </summary> 
    IsNotNull,

    /// <summary>
    /// Value is between the two supplied bounds.
    /// </summary> 
    Between
}
