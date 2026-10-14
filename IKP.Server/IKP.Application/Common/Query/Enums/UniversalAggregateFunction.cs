namespace IKP.Application.Common.Query.Enums;

/// <summary>
/// Defines aggregate functions available to universal grouped queries.
/// </summary>
public enum UniversalAggregateFunction
{
    /// <summary>
    /// Counts rows.
    /// </summary> 
    Count,

    /// <summary>
    /// Counts distinct non-null values.
    /// </summary> 
    CountDistinct,

    /// <summary>
    /// Calculates the sum of a numeric field.
    /// </summary> 
    Sum,

    /// <summary>
    /// Calculates the average of a numeric field.
    /// </summary> 
    Average,

    /// <summary>
    /// Calculates the minimum value of a numeric field.
    /// </summary> 
    Minimum,

    /// <summary>
    /// Calculates the maximum value of a numeric field.
    /// </summary> 
    Maximum
}
