namespace IKP.Infrastructure.Persistence.DatabaseKeys;

/// <summary>
/// Contains database schema names used by the application.
/// </summary>
public static class DatabaseSchemas
{
    private const string LookupPrefix = "lookup";
    private const string Diagnostics = "diagnostics";


    /// <summary>
    /// Gets the physical file metadata schema.
    /// </summary>
    public const string Files = "files";

    /// <summary>
    /// Gets the main application schema.
    /// </summary>
    public const string App = "app";

    /// <summary>
    /// Gets the support schema.
    /// </summary>
    public const string Support = "support";

    /// <summary>
    /// Gets the authentication schema.
    /// </summary>
    public const string Auth = "auth";

    /// <summary>
    /// Gets the support lookup schema.
    /// </summary>
    public const string SupportLookup = $"{Support}_{LookupPrefix}";

    /// <summary>
    /// Gets the authentication lookup schema.
    /// </summary>
    public const string AuthLookup = $"{Auth}_{LookupPrefix}";

    /// <summary>
    /// Gets the audit lookup schema.
    /// </summary>
    public const string LogsLookup = $"{Logs}_{LookupPrefix}";

    /// <summary>
    /// Gets the diagnostics lookup schema.
    /// </summary>
    public const string DiagnosticsLookup = $"{Diagnostics}_{LookupPrefix}";

    /// <summary>
    /// Gets the audit log schema.
    /// </summary>
    public const string Logs = $"{App}_logs";

    /// <summary>
    /// Gets the diagnostics core schema.
    /// </summary>
    public const string DiagnosticsCore = $"{Diagnostics}_core";

    /// <summary>
    /// Gets the diagnostics statistics schema.
    /// </summary>
    public const string DiagnosticsStatistics = $"{Diagnostics}_statistics";
}
