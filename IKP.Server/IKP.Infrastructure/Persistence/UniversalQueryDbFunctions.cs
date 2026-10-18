using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence;

/// <summary>
/// Provides database functions used by universal queries.
/// </summary>
public static class UniversalQueryDbFunctions
{
    /// <summary>
    /// Calculates the PostgreSQL fuzzystrmatch Levenshtein distance.
    /// </summary>
    [DbFunction("levenshtein", IsBuiltIn = true)]
    public static int Levenshtein(string source, string target) => throw new InvalidOperationException("This method is intended for use in LINQ queries.");
}
